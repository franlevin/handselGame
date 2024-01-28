using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    [SerializeField] private float sceneLoadWaitTime = 1f;

    // Event for shouting "ELEVATOR STATE!" to all gameObjects
    public static event Action ElevatorState;

    // Event for shouting "RUNNING STATE!" to all gameObjects
    //public static event Action RunningState;

    // Event for shouting "GAME OVER STATE!" to all gameObjects
    //public static event Action OverState;

    // Start is called before the first frame update
    void Start()
    {
        ElevatorTrigger.PlayerEnteredElevator += ActivateElevatorState;
    }

    void ActivateElevatorState()
    {
        gameState = GameState.Elevator;
        ElevatorState?.Invoke();
        Debug.Log("ELEVATOR STATE BRUH");
    }

    public void ReloadScene(){
        StartCoroutine(LoadSceneDelayed(SceneManager.GetActiveScene().buildIndex));
    }

    private IEnumerator LoadSceneDelayed(int sceneBuildIndex){
        yield return new WaitForSeconds(sceneLoadWaitTime);
        SceneManager.LoadScene(sceneBuildIndex);
    }

}
