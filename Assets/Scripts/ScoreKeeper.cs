using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreKeeper : MonoBehaviour {
    
    [SerializeField] public static int currentPoints = 0;

    [Header("UI")]
    [SerializeField] TextMeshProUGUI scoreField;

    private void Awake() {
        print("scorekeeper awake.  Current Score is: "+currentPoints);
        scoreField = GameObject.FindGameObjectWithTag("ScoreDisplay").GetComponent<TextMeshProUGUI>();
        SetScore();

        /*
        int gcCount = GameObject.FindObjectsOfType<ScoreKeeper>().Length;
        if (gcCount > 1) {
            Destroy(gameObject);
        } else {
            DontDestroyOnLoad(gameObject);
        }
        */
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
        //Destroy(gameObject);
    }

}