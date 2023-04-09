using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorTrigger : MonoBehaviour
{


    public static event Action PlayerEnteredElevator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerEnteredElevator?.Invoke();
            Debug.Log("Trigger con player de elevator");
            Destroy(gameObject);
        }
    }
}
