using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainModule : MonoBehaviour
{
    [SerializeField] public TerrainModuleData data;
    [SerializeField] private float length;
    [SerializeField] private float height;
    private float destroyDistance = 100f;
    [SerializeField] private GameObject mainFloor;          // Lowest floor on module for player collision
    [SerializeField] private GameObject Player;

    public enum ModuleType // your custom enumeration
    {
        Normal,
        Void
    };

    [SerializeField] public ModuleType moduleType;

    public static event Action<float> TerrainModuleDestroyed;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        length = data.length;

        Debug.Log("moduleType is:  " + moduleType);
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

