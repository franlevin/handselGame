using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIScore : MonoBehaviour
{
    private ScoreKeeper scoreKeeper;
    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI scoreUpdateNotification;
    [SerializeField] private float scoreUpdateDisplayTime = 1;

    private void Awake() {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        scoreText = GetComponentsInChildren<TextMeshProUGUI>()[0];
        scoreUpdateNotification = GetComponentsInChildren<TextMeshProUGUI>()[1];
    }

     private void Update() {
        UpdateScoreText();
    }

    public void UpdateScoreText(){
        int score = Mathf.RoundToInt(scoreKeeper.GetScore());
        scoreText.text = score.ToString("000000000");
    }

    public void NotifyScoreIncrease(int points){
        StartCoroutine(ScoreIncreaseNotificationDisplayDelay(points));
    }

    private IEnumerator ScoreIncreaseNotificationDisplayDelay(int points){
        scoreUpdateNotification.text = "+" + points;
        yield return new WaitForSeconds(scoreUpdateDisplayTime);
        scoreUpdateNotification.text = "";
    }
}
