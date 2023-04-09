using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject Player;
    public enum GameState // your custom enumeration
    {
        Running,
        Elevator,
        Over
    };

    [SerializeField] public GameState gameState;

    // Event for shouting "ELEVATOR STATE!" to all gameObjects
    public static event Action ElevatorState;

    // Start is called before the first frame update
    void Start()
    {
        ElevatorTrigger.PlayerEnteredElevator += ActivateElevatorState;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ActivateElevatorState()
    {
        gameState = GameState.Elevator;
        ElevatorState?.Invoke();
    }
}
