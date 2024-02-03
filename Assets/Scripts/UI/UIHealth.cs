using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class UIHealth : MonoBehaviour
{
    
    [SerializeField] private GameObject healthIcon;
    private Health playerHealth;

    private void Awake(){
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
    }
    
    public void IncreaseHealthUI()
    {
        int currentHealth = transform.childCount;
        Instantiate(healthIcon, transform);
    }

    public void DecreaseHealthUI(){
        int currentHealth = transform.childCount;
        if(currentHealth > 0){
            Destroy(transform.GetChild(currentHealth -1).gameObject);
        }
        
    }

    public void InitializeHealthUI(){
        int hitPoints = playerHealth.GetHitPoints();
        for(int healthIconNumber = 1; healthIconNumber <= hitPoints; healthIconNumber++){
            Instantiate(healthIcon, transform);
        }
    }
    
}
