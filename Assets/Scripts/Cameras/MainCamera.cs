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
    
    //private float xPos;
    //private float playerPos;

    // Start is called before the first frame update
    void Start()
    {
        _gameObject = GameObject.FindGameObjectWithTag("Player");
        //cameraSpeed = _gameObject.GetSpeed();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        // transform.position += new Vector3(_gameObject.transform.position.x, 0, 0);
        Vector3 targetPosition = new Vector3(_gameObject.transform.position.x + 30, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * followSpeed);

    }
}
