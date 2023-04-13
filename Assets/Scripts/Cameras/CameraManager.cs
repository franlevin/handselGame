using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform CamTransform;
    public Transform Player;
    float followspeed;

    public enum CameraMovementType // your custom enumeration
    {
        OneAxis,
        TwoAxis
    };

    private CameraMovementType movementType;

    void Start()
    {
        movementType = CameraMovementType.OneAxis;
        GameManager.ElevatorState += ActivateTwoAxisCamMovement;
    }

    void Update()
    {
        Debug.Log("Player pos is: " + Player.transform.position);
        Debug.Log("Camera pos is: " + transform.position);
    }

    private void FixedUpdate()
    {
        CameraMovement();
    }

    private void CameraMovement()
    {
        Vector2 targetPosition = new(0,0);

        switch (movementType)
        {
            case CameraMovementType.OneAxis:
                targetPosition = new Vector2(Player.position.x, CamTransform.position.y);
                break;
            case CameraMovementType.TwoAxis:
                targetPosition = new Vector2(Player.position.x, Player.position.y);
                break;
        }

        CamTransform.position = Vector2.Lerp(CamTransform.position, targetPosition, Time.deltaTime * followspeed);

    }

    /* private void ActivateOneAxisCamMovement()
    {

    } */

    private void ActivateTwoAxisCamMovement()
    {
        movementType = CameraMovementType.TwoAxis;
        Debug.Log("Se activa two axis");
    }
}
