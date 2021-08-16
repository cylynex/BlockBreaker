using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeContainer : MonoBehaviour {

    [SerializeField] public float descentSpeed = 1f;
    [SerializeField] float upgradeTextTime = 2f;
    [SerializeField] Upgrade[] upgrades;
    [SerializeField] Upgrade selectedUpgrade;

    [Header("Upgrade Prefabs")]
    [SerializeField] GameObject newBall;
    [SerializeField] Vector2 newBallSpawnMin;
    [SerializeField] Vector2 newBallSpawnMax;

    // References
    ScoreKeeper scoreKeeper;
    GameController gameController;


    private void Start() {
        GetReferences();
        SetupContainer();
        PickUpgrade();
    }

    private void GetReferences() {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        gameController = FindObjectOfType<GameController>();
    }

    private void SetupContainer() {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, -descentSpeed);
    }

    private void PickUpgrade() {
        selectedUpgrade = upgrades[Random.Range(0, upgrades.Length)];
        GetComponent<SpriteRenderer>().color = selectedUpgrade.upgradeColor;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Paddle") {
            Destroy(gameObject,0.25f);

            // Add Points
            scoreKeeper.AddPoints(selectedUpgrade.freePoints);

            // Spawn extra balls
            if (selectedUpgrade.ballsToSpawn > 0) {
                SpawnExtraBalls();
            }

            // Paddle Scaling
            if (selectedUpgrade.paddleScale > 0) {
                FindObjectOfType<Paddle>().transform.localScale = new Vector3(selectedUpgrade.paddleScale, 1, 1);
            }

            // Display game text from upgrade
            ShowText();

        }

        else if (collision.gameObject.tag == "Destroyer") {
            Destroy(gameObject, 2f);
        }
    }

    void SpawnExtraBalls() {
        for (int ball = 0; ball < selectedUpgrade.ballsToSpawn; ball++) {
            GameObject thisNewBall = Instantiate(newBall, transform.position, transform.rotation);
            Vector2 ballSpawnPower = new Vector2(Random.Range(newBallSpawnMin.x, newBallSpawnMin.y), Random.Range(newBallSpawnMax.y, newBallSpawnMax.y));
            thisNewBall.GetComponent<Ball>().isBallDocked = false;
            thisNewBall.GetComponent<Rigidbody2D>().velocity = ballSpawnPower;
            gameController.AddBallToPlay();

            // Check duration
            if (selectedUpgrade.upgradeDuration > 0) {
                thisNewBall.GetComponent<Ball>().ballhasDuration = true;
                thisNewBall.GetComponent<Ball>().ballDuration = selectedUpgrade.upgradeDuration;
                thisNewBall.GetComponent<Ball>().StartDurationTimer();
                thisNewBall.GetComponent<SpriteRenderer>().color = selectedUpgrade.upgradeAltColor;
            }
        }
    }

    void ShowText() {
        gameController.upgradeText.text = selectedUpgrade.upgradeFlavorText.ToString();
        gameController.upgradeTextTimer = upgradeTextTime;
    }

}