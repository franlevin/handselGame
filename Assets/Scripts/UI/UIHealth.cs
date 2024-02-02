using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class UIHealth : MonoBehaviour
{
    
    [SerializeField] private GameObject healthIcon;
    [SerializeField] private float iconPadding = 0f;
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
        Destroy(transform.GetChild(currentHealth -1).gameObject);
    }

    public void InitializeHealthUI(){
        int hitPoints = playerHealth.GetHitPoints();
        for(int healthIconNumber = 1; healthIconNumber <= hitPoints; healthIconNumber++){
            Instantiate(healthIcon, transform);
        }
    }
    
}
