using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class MainCamera : MonoBehaviour
{
    //[SerializeField] private Camera _camera;

    [SerializeField] private GameObject _gameObject;
    [SerializeField] private float followSpeed = 5f;

    public enum CameraMovementType // your custom enumeration
    {
        OneAxis,
        TwoAxis
    };

    private CameraMovementType movementType;

    //private float xPos;
    //private float playerPos;

    // Start is called before the first frame update
    void Start()
    {
        _gameObject = GameObject.FindGameObjectWithTag("Player");
        movementType = CameraMovementType.OneAxis;
        GameManager.ElevatorState += ActivateTwoAxisCamMovement;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        // transform.position += new Vector3(_gameObject.transform.position.x, 0, 0);


        CameraMovement();


        //Vector3 targetPosition = new Vector3(_gameObject.transform.position.x + 30, transform.position.y, transform.position.z);
        //transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * followSpeed);

    }

    private void CameraMovement()
    {
        Vector3 targetPosition = new();

        switch (movementType)
        {
            case CameraMovementType.OneAxis:
                targetPosition = new Vector3(_gameObject.transform.position.x + 30, transform.position.y, transform.position.z);
                break;
            case CameraMovementType.TwoAxis:
                targetPosition = new Vector3(_gameObject.transform.position.x + 30, _gameObject.transform.position.y, transform.position.z);
                break;
        }

        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * followSpeed);
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
