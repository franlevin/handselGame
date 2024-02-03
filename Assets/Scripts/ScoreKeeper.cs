using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    private float score;
    [SerializeField] private float scoreIncreasePerSecond;

    void Update(){
        IncreaseScore();
    }

    private void IncreaseScore(){
        score += scoreIncreasePerSecond * Time.deltaTime;
    }

    public float GetScore(){
        return score;
    }

}
