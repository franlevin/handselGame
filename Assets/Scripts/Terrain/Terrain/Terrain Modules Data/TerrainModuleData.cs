using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Terrain Module", menuName = "TerrainModule")]
public class TerrainModuleData : ScriptableObject
{
    [SerializeField]
    public float length;

    [SerializeField]
    public string description;
}
