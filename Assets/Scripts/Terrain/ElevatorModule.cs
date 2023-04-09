using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorModule : MonoBehaviour
{
    [SerializeField] public ElevatorModuleData data;

    // Scriptable Object Data
    private bool direction;
    private float heightToTravel;
    private float maxTravelSeconds;
    private float speed;

    // Instance Data
    private float secondsTraveled;
    private bool isPlayerOnBoard;
    private bool isTraveling;

    // Start is called before the first frame update
    void Start()
    {
        AssignDataVariables();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        ElevationControl();
    }

    void AssignDataVariables()
    {
        direction = data.direction;
        heightToTravel = data.heightToTravel;
        maxTravelSeconds = data.maxTravelSeconds;
        speed = data.speed;
    }

    void ElevationControl ()
    {
        // if player is on board && seconds traveled less than max seconds
            // move elevator

    }

}
