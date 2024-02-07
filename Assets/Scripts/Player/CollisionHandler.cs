using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class CollisionHandler : MonoBehaviour
{
    private Health health;
    private Rigidbody2D rigidBody;
    private GameManager gameManager;
    private UIUpdate UIUpdate;
    [SerializeField] private ParticleSystem particleEffect;

    public static event Action PlayerDeath;

    void Awake(){
        rigidBody = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();
        gameManager = FindObjectOfType<GameManager>();
        UIUpdate = FindObjectOfType<UIUpdate>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Hazards"){
            if(health.TakeHit()){
                PlayParticleEffect();
                UIUpdate.DecreaseHealthUI();
                if(!health.IsAlive()){
                    gameManager.ReloadScene();
                    PlayerDeath?.Invoke();
                }
            }
            
        }
    }

    private void PlayParticleEffect(){
        if(!particleEffect.isPlaying){
            particleEffect.Play();
        }
    }


}
