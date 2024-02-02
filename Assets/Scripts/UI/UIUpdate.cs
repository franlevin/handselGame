using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIUpdate : MonoBehaviour
{
    private UIHealth UIHealth;

    void Awake(){
        UIHealth = GetComponentInChildren<UIHealth>();
    }
    void Start()
    {
        UIHealth.InitializeHealthUI();
    }

    public void DecreaseHealthUI(){
        UIHealth.DecreaseHealthUI();
    }

    

}
