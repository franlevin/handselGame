using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteScroller : MonoBehaviour
{
    [SerializeField] private Vector2 moveSpeed;
    private Vector2 offset;
    private Material material;
    private bool scroll = true;

    void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
        CollisionHandler.PlayerDeath += StopScroll;
    }

    // Update is called once per frame
    void Update()
    {
        if(scroll){
            offset = moveSpeed * Time.deltaTime;
            material.mainTextureOffset += offset;
        }
        
    }

    private void StopScroll(){
        scroll = false;
    }

}
