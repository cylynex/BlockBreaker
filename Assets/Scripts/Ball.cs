using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    [Header("References")]
    [SerializeField] Paddle paddle;
    [SerializeField] AudioClip[] hitSounds;
    [SerializeField] AudioSource audioSource;
    [SerializeField] GameController gameController;

    [Header("Upgrade Data")]
    [SerializeField] public bool ballhasDuration = false;
    [SerializeField] public float ballDuration = 0f;
    
    // Internal Only
    Vector2 paddleToBall;
    public bool isBallDocked = true;
    Rigidbody2D rb;

    [Header("Adjustments")]
    [SerializeField] float randomAdjustment = 1f;
    [SerializeField] Vector2 ballMinSpeed = new Vector2(1.5f, 1.5f);

    private void Start() {
        GetReferences();
        SetBallPosition();
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void GetReferences() {
        paddle = GameObject.FindGameObjectWithTag("Paddle").GetComponent<Paddle>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    private void SetBallPosition() {
        paddleToBall = new Vector2(0, 0.4f);
        paddleToBall = transform.position - paddle.transform.position;
    }

    private void Update() {
        if (isBallDocked) {
            LockBallToPaddle();
            LaunchBall();
        } else {
            AutoPlay();
        }

        if (ballhasDuration) {
            ballDuration -= Time.deltaTime;
            if (ballDuration <= 0) {
                // Time has run out on this ball, delete it and subtract from the count.
                gameController.RemoveBallFromPlay();
                Destroy(gameObject);
            }
        }
    }

    void AutoPlay() {
        if (gameController.autoPlay == true) {
            float posX = gameObject.transform.position.x;
            Vector2 movePaddleTo = new Vector2(posX, paddle.transform.position.y);
            paddle.transform.position = movePaddleTo;
        }
    }

    void LockBallToPaddle() {
        Vector2 paddlePosition = new Vector2(paddle.transform.position.x, paddle.transform.position.y);
        transform.position = paddlePosition + paddleToBall;
    }

    void LaunchBall() {
        if (isBallDocked) {
            if (Input.GetMouseButtonDown(0)) {
                isBallDocked = false;
                rb.velocity = new Vector2(0f, 6f);
            }
        }
    }

    public void StartDurationTimer() {

    }

    private void OnCollisionEnter2D(Collision2D collision) {

        // Randomize ball direction slightly
        Vector2 velocityAdjustment = new Vector2(Random.Range(0f, randomAdjustment), Random.Range(0f, randomAdjustment));
        rb.velocity += velocityAdjustment;

        if (collision.gameObject.tag == "BasicBlock") {
            audioSource.PlayOneShot(clipToPlay);
        }
    }

}