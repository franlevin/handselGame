using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Elevator Module", menuName = "ElevatorModule")]
public class ElevatorModuleData : ScriptableObject
{
    [Header("Direction is true for up, false for down")]
    [SerializeField] public bool direction;

    [Header("Direction is true for up, false for down")]
    [SerializeField] public float heightToTravel;

    [Header("Direction is true for up, false for down")]
    [SerializeField] public float maxTravelSeconds;

    [Header("Direction is true for up, false for down")]
    [SerializeField] public float speed;

    [SerializeField]
    public float length;

    [SerializeField]
    public string description;
}
