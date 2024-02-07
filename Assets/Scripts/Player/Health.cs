using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int hitPoints = 3;
    [SerializeField] private float immunityTime = 1f;
    [SerializeField] private float spriteBlinkFrecuency = 0.5f;
    private SpriteRenderer spriteRenderer;
    
    private bool immune = false;

    private void Awake() {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    
    public bool TakeHit(){
        bool damageTaken = !immune;
        if(damageTaken){
            hitPoints--;
            StartCoroutine(TriggerImmunity());
            StartCoroutine(MakeSpriteBlink());
        } return damageTaken;
    }

    private IEnumerator TriggerImmunity(){
        immune = true;
        yield return new WaitForSeconds(immunityTime);
        immune = false;

    }

    public bool IsAlive(){
        return hitPoints > 0;
    }

    public float GetImmunityTime(){
        return immunityTime;
    }

        private IEnumerator MakeSpriteBlink(){
        float elapsedBlinkingTime = 0f;
        while(elapsedBlinkingTime < immunityTime && immune){
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(spriteBlinkFrecuency);
            elapsedBlinkingTime += Time.deltaTime;
        }
        spriteRenderer.enabled = true;
    }

    public int GetHitPoints(){
        return hitPoints;
    }


}
