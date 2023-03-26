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
        if (IsPlayerFarAway(player.transform.localPosition))
        {
            TerrainModuleDestroyed?.Invoke(length);
            Destroy(gameObject);
            Debug.Log("Se destruye un terreno");
        }
    }

    private bool IsPlayerFarAway (Vector2 playerX)
    {
        //Debug.Log("Player x is: " + (playerX) + " and Module x is: " + transform.localPosition.x + " , so Player x - Module x is: " + (playerX - transform.localPosition.x));

        float distance = Vector2.Distance(playerX, transform.localPosition);

        Debug.Log("Distance es: " + distance + ", Player x is: " + (playerX) + " and Module x is: " + transform.localPosition.x + ", Retorna: " + (distance > destroyDistance && playerX.x > transform.localPosition.x)+ "Distance es mayor a destroyDistance: " + (distance > destroyDistance));

        return (distance > destroyDistance && playerX.x > transform.localPosition.x);
    }
}

