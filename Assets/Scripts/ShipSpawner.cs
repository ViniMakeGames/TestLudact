using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShipSpawner : MonoBehaviour
{
    [SerializeField] 
    private GameObject _originalShip;
    [SerializeField]
    private float _spawnArea;
    [SerializeField]
    private int _generationTime;

    private int _currentGeneration = 1;
    private float _generationTimer;

    private int _maxIterations = 10;

    private readonly List<GameObject> _ships = new ();
    private float _removeShipTimer;

    private int _shipAmount = 0;

    [SerializeField]
    private GameUI _gameUI;

    private int _spawnedShips;

    private void Start()
    {
        //Get all ships that are already in the map.
        for (var i = 0; i < transform.childCount; i++)
        {
            _ships.Add(transform.GetChild(i).gameObject);
            _ships[^1].SetActive(false);
        }
        
        _gameUI.UpdateTotalSpaceShips(_spawnedShips);
        _gameUI.UpdateShipsOnScreen(_shipAmount);
        _gameUI.UpdateInstancesUsed(_ships.Count);
        _gameUI.UpdateFibonacciIteration(_currentGeneration, GetFibonacci(_currentGeneration));
    }

    private void Update()
    {
        RemoveShips();

        if(_currentGeneration < _maxIterations)
            GenerateSpaceShips();
    }

    //Generate new spaceships using the current fibonacci value.
    private void GenerateSpaceShips()
    {
        if (_generationTimer < _generationTime)
        {
            _generationTimer += Time.deltaTime;
            return;
        }

        var pos = transform.position;
        var fibonacci = GetFibonacci(_currentGeneration);
        
        for (var i = 0; i < fibonacci; i++)
        {
            var ship = SpawnShip();
            ship.transform.position = new Vector3(pos.x + Random.Range(-_spawnArea, _spawnArea), pos.y);
            _shipAmount += 1;
            _spawnedShips += 1;
        }

        _generationTimer = 0;
        _currentGeneration += 1;
        
        _gameUI.UpdateTotalSpaceShips(_spawnedShips);
        _gameUI.UpdateShipsOnScreen(_shipAmount);
        _gameUI.UpdateInstancesUsed(_ships.Count);
        _gameUI.UpdateFibonacciIteration(_currentGeneration, fibonacci);
    }

    //Removes one ship per second.
    private void RemoveShips()
    {
        if (_shipAmount <= 0) return;
        
        if (_removeShipTimer < 1)
        {
            _removeShipTimer += Time.deltaTime;
            return;
        }
        
        foreach (var ship in _ships.Where(ship => ship.activeSelf))
        {
            ship.gameObject.SetActive(false);
            DecreaseShipCount();
            _removeShipTimer = 0;
            return;
        }
    }

    //Get an available ship or instantiate a new one if all ships are being used.
    private GameObject SpawnShip()
    {
        foreach (var ship in _ships.Where(ship => !ship.activeSelf))
        {
            ship.SetActive(true);
            return ship.gameObject;
        }

        var newShip = Instantiate(_originalShip, transform);
        newShip.SetActive(true);
        _ships.Add(newShip);
        
        return newShip;
    }

    //Recursive function to get the current fibonacci iteration.
    private int GetFibonacci(int n)
    {
        return n switch
        {
            <= 0 => 0,
            1 => 1,
            _ => GetFibonacci(n - 1) + GetFibonacci(n - 2)
        };
    }

    //Debug function to show the spawn area in the editor.
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        var pos = transform.position;
        Gizmos.DrawLine(new Vector3(pos.x - _spawnArea, pos.y, 0), new Vector3(pos.x + _spawnArea, pos.y, 0));
    }

    public void DecreaseShipCount()
    {
        _shipAmount -= 1;
        _gameUI.UpdateShipsOnScreen(_shipAmount);
    }
}
