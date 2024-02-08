using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIUpdate : MonoBehaviour
{
    private UIHealth UIHealth;
    private UIScore UIScore;

    void Awake(){
        UIHealth = GetComponentInChildren<UIHealth>();
        UIScore = GetComponentInChildren<UIScore>();
    }
    void Start()
    {
        UIHealth.InitializeHealthUI();
    }
    public void DecreaseHealthUI(){
        UIHealth.DecreaseHealthUI();
    }

    public void NotifyScoreIncrease(int points){
        UIScore.NotifyScoreIncrease(points);
    }

    

}
