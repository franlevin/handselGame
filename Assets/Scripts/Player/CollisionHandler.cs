using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private Health health;
    private Rigidbody2D rigidBody;
    private GameManager gameManager;
    [SerializeField] private ParticleSystem particleEffect;

    void Awake(){
        rigidBody = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();
        gameManager = FindObjectOfType<GameManager>();

    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Hazards"){
            health.TakeHit();
            PlayParticleEffect();
            if(!health.IsAlive()){
                gameManager.ReloadScene();
            }
        }
    }

    private void PlayParticleEffect(){
        if(!particleEffect.isPlaying){
            particleEffect.Play();
        }
    }


}
