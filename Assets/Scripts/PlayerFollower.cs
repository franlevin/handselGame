using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
    private GameObject player;
    
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
    }

    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, transform.position.y);
    }
}
