using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainModule : MonoBehaviour
{
    //[SerializeField] public TerrainModuleData data;
    [SerializeField] private float length;
    [SerializeField] private float height;
    private float destroyDistance = 300f;
    [SerializeField] private GameObject mainFloor;      // Lowest floor on module for player collision
    [SerializeField] private GameObject player;
    
    public enum ModuleType // your custom enumeration
    {
        Normal,
        Void,
        Elevator
    };

    [SerializeField] public ModuleType moduleType;

    public static event Action<float> TerrainModuleDestroyed;

    public float GetLength()
    {
        return length;
    }

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        length = GetComponent<BoxCollider2D>().size.x;

        Debug.Log("moduleType is:  " + moduleType);
    }

    void Update()
    {
        if (player != null) { CheckDestruction(player); }
    }
    private void CheckDestruction(GameObject player)
    {
        if (IsPlayerFarAway(player.transform.localPosition))
        {
            TerrainModuleDestroyed?.Invoke(length);
            Destroy(gameObject);
        }
    }

    private bool IsPlayerFarAway (Vector2 playerX)
    {
        float distance = Vector2.Distance(playerX, transform.localPosition);

        return (distance > destroyDistance && playerX.x > transform.localPosition.x);
    }

    

}

