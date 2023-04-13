using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class ElevatorModule : TerrainModule
{
    [SerializeField] public ElevatorModuleData elevatorData;

    // Scriptable Object Data
    private bool direction;
    private float heightToTravel;
    private float maxTravelSeconds;
    private float speed;

    // Instance Data
    private float secondsTraveled = 0f;
    private bool isPlayerOnBoard;
    private bool isTraveling;

    // Start is called before the first frame update
    void Start()
    {
        AssignDataVariables();
        ElevatorTrigger.PlayerEnteredElevator += StartElevator;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        ElevationControl();
    }

    private void AssignDataVariables()
    {
        direction = elevatorData.direction;
        heightToTravel = elevatorData.heightToTravel;
        maxTravelSeconds = elevatorData.maxTravelSeconds;
        speed = elevatorData.speed;
    }

    private void ElevationControl ()
    {
        if (isTraveling && secondsTraveled < maxTravelSeconds)
        {
            Vector2 pos = transform.position;
            pos += Vector2.up * Time.deltaTime * speed;
            transform.position = pos;
 
            secondsTraveled += Time.fixedDeltaTime;
        }
        // if player is on board && seconds traveled less than max seconds
            // move elevator

    }

    private void StartElevator()
    {
        isTraveling = true;
    }
}

