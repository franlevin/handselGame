using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class HUDManager : MonoBehaviour
{
    public UIHealth healthUI;
    public UIScore scoreUI;

    void Awake()
    {
       healthUI = FindObjectOfType<UIHealth>();
       scoreUI = FindObjectOfType<UIScore>();

    }   

    // Start is called before the first frame update
    void Start()
    {
        CollisionHandler.PlayerDeath += ActivateDeathHUD;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ActivateDeathHUD(){
        if (healthUI != null)
        {
            healthUI.gameObject.SetActive(false);
        }

        if (scoreUI != null)
        {
            scoreUI.gameObject.SetActive(false);
        }
    }
}
