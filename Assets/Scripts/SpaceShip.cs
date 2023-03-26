using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : MonoBehaviour
{
    [SerializeField]
    private float _minBaseSpeed;
    [SerializeField] 
    private float _maxBaseSpeed;
    [SerializeField]
    private float _acceleration;

    private const float MaxDistance = -12;

    private float _currentSpeed;

    [SerializeField]
    private ShipSpawner _shipSpawner;
    
    private void OnEnable()
    {
        _currentSpeed = Random.Range(_minBaseSpeed, _maxBaseSpeed);
    }
    
    private void Update()
    {
        _currentSpeed += _acceleration;
        transform.Translate(0,0,_currentSpeed * Time.deltaTime,Space.Self);

        if (transform.position.y < MaxDistance)
        {
            _shipSpawner.DecreaseShipCount();
            gameObject.SetActive(false);
        }
    }
}
