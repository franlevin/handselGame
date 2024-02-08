using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIScore : MonoBehaviour
{
    private ScoreKeeper scoreKeeper;
    private TextMeshProUGUI scoreText;

    private void Awake() {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        scoreText = GetComponentInChildren<TextMeshProUGUI>();
    }

     private void Update() {
        UpdateScoreText();
    }

    public void UpdateScoreText(){
        int score = Mathf.RoundToInt(scoreKeeper.GetScore());
        scoreText.text = "Score: " + score.ToString("000000000");
    }
}
