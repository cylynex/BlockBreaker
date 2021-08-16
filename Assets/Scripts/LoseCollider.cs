using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseCollider : MonoBehaviour {

    GameController gameController;

    private void Start() {
        gameController = FindObjectOfType<GameController>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Ball") {

            // Subtract ball in play.  If its the last ball, lose.
            gameController.RemoveBallFromPlay();
            Destroy(collision.gameObject);
        }
    }
    
}