using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreKeeper : MonoBehaviour {
    
    [SerializeField] public static int currentPoints = 0;

    [Header("UI")]
    [SerializeField] TextMeshProUGUI scoreField;

    private void Awake() {
        scoreField = GameObject.FindGameObjectWithTag("ScoreDisplay").GetComponent<TextMeshProUGUI>();
        SetScore();
    }

    public void AddPoints(int pointsToAdd) {
        currentPoints += pointsToAdd;
        SetScore();
    }

    public void SetScore() {
        scoreField.text = currentPoints.ToString();
    }

    public void ResetScore() {
        currentPoints = 0;
        SetScore();
    }

}