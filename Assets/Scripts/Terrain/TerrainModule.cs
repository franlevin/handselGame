using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainModule : MonoBehaviour
{
    [SerializeField] public TerrainModuleData data;
    [SerializeField] private float length;
    [SerializeField] private float height;
    [SerializeField] private float destroyDistance = 4444f;
    [SerializeField] private GameObject mainFloor;          // Lowest floor on module for player collision
    [SerializeField] GameObject Player;

    public static event Action<float> TerrainModuleDestroyed;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        length = data.length;
        Debug.Log("El length es:" + length);
    }

    // Update is called once per frame
    void Update()
    {
        if (Player != null) { CheckDestruction(Player); }
    }

    public float GetLength()
    {
        return length;
    }

    private void CheckDestruction(GameObject player)
    {
        if (IsPlayerFarAway(player.transform.position.x))
        {
            TerrainModuleDestroyed?.Invoke(length);
            Destroy(gameObject);
            Debug.Log("Se destruye un terreno");
        }
    }

    private bool IsPlayerFarAway (float playerX)
    {
        return ((playerX - transform.position.x) > destroyDistance);
    }
}

