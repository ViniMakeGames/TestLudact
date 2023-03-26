using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _totalSpaceships;
    [SerializeField]
    private TextMeshProUGUI _shipsOnScreen;
    [SerializeField]
    private TextMeshProUGUI _instancesUsed;
    [SerializeField]
    private TextMeshProUGUI _fibonacciIteration;

    public void UpdateTotalSpaceShips(int amount) => _totalSpaceships.text = $"Total Spaceships:\n{amount}";
    public void UpdateShipsOnScreen(int amount) => _shipsOnScreen.text = $"Ships on Screen:\n{amount}";
    public void UpdateInstancesUsed(int amount) => _instancesUsed.text = $"Obj Instances Used:\n{amount}";
    public void UpdateFibonacciIteration(int iteration, int quantity) =>
        _fibonacciIteration.text = $"Fibonacci Iteration:\n{iteration} ({quantity})";
}
