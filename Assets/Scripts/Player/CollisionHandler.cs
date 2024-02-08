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
    private AnimationManager animationManager;
    [SerializeField] private ParticleSystem particleEffect;

    public static event Action PlayerDeath;

    void Awake(){
        rigidBody = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();
        gameManager = FindObjectOfType<GameManager>();
        UIUpdate = FindObjectOfType<UIUpdate>();
        animationManager = FindObjectOfType<AnimationManager>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        switch(other.tag){
            
            case "Hazards":
                if(health.TakeHit()){
                    PlayParticleEffect();
                    UIUpdate.DecreaseHealthUI();
                    if(!health.IsAlive()){
                        //gameManager.ReloadScene();
                        PlayerDeath?.Invoke();
                    }
                }
            break;

            case "Void":
                gameManager.ReloadScene();
            break;
        }
            
    }

    private void PlayParticleEffect(){
        if(!particleEffect.isPlaying){
            particleEffect.Play();
        }
    }


}
