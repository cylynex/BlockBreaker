using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    [Header("Game Data")]
    [SerializeField] int breakableBlocks;
    SceneLoader sceneLoader;
    [SerializeField] int ballsInPlay = 1;

    [Header("Settings")]
    [Range(0,1)][SerializeField] float gameSpeed = 1f;
    [SerializeField] public bool autoPlay = true;

    [Header("UI")]
    [SerializeField] public TextMeshProUGUI upgradeText;
    public float upgradeTextTimer = 0f;

    private void Awake() {
        sceneLoader = GetComponent<SceneLoader>();
        upgradeText = GameObject.FindGameObjectWithTag("UpgradeText").GetComponent<TextMeshProUGUI>();
        upgradeText.text = "".ToString();
    }

    public void AddBlockToCount() {
        breakableBlocks++;
    }

    public void RemoveBlockFromCount() {
        breakableBlocks--;

        // Check for finish
        if (breakableBlocks <= 0) {
            sceneLoader.LoadNextScene();
        }
    }

    private void Update() {
        Time.timeScale = gameSpeed;
        if (upgradeTextTimer > 0) {
            upgradeTextTimer -= Time.deltaTime;
            if (upgradeTextTimer <= 0) {
                upgradeText.text = "".ToString();
            }
        }
    }

    public void AddBallToPlay() {
        ballsInPlay++;
    }

    public void RemoveBallFromPlay() {
        ballsInPlay--;
        if (ballsInPlay == 0) {
            SceneManager.LoadScene("Lose");
        }
    }

    

}