using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainModule : MonoBehaviour
{
    private GameObject player;
    private float length;
    private float destroyDistance = 30f;

    public static event Action<float> TerrainModuleDestroyed;

    public float GetLength()
    {
        return length;
    }

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        length = GetComponent<BoxCollider2D>().size.x;
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

