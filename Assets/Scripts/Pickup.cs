using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private ScoreKeeper scoreKeeper;
    private UIUpdate UIUpdate;
    [SerializeField] private int points = 100;

    private void Awake() {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        UIUpdate = FindObjectOfType<UIUpdate>();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        scoreKeeper.IncreaseScore(points);
        Destroy(gameObject);
        UIUpdate.NotifyScoreIncrease(points);
    }
}
