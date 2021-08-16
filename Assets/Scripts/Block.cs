using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

    [Header("Config")]
    [SerializeField] int blockPointValue;
    [SerializeField] AudioClip breakSound;
    [SerializeField] GameObject blockParticles;
    [SerializeField] int maxHits;
    [SerializeField] int upgradeChance = 100;
    [SerializeField] bool hasUpgrade = false;
    [SerializeField] GameObject upgradeContainer;

    [Header("Semi Config")]
    [SerializeField] Sprite[] hitSprites;

    // Internal Only
    GameController gameController;
    ScoreKeeper scoreKeeper;
    int currentHits;
    
    private void Start() {
        GetReferences();
        if (gameObject.tag == "BlockBreakable") {
            gameController.AddBlockToCount();
            SetupUpgrade();
        }
    }


    void SetupUpgrade() {
        if (upgradeChance > 0) {
            int roll = Random.Range(0, 100);
            if (roll <= upgradeChance) {
                // Item has an upgrade in it
                hasUpgrade = true;
            }
        }
    }


    void GetReferences() {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        scoreKeeper = GameObject.FindObjectOfType<ScoreKeeper>();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        
        if (gameObject.tag == "BlockBreakable") {
            HitBlock();
        } 
    }

    void HitBlock() {
        currentHits++;
        if (currentHits == maxHits) {
            DestroyBlock();
        } else {
            if (hitSprites.Length > 0) {
                GetComponent<SpriteRenderer>().sprite = hitSprites[currentHits - 1];
            }
        }
    }

    void DestroyBlock() {
        gameController.RemoveBlockFromCount();
        scoreKeeper.AddPoints(blockPointValue);
        TriggerParticles();
        DispenseUpgrade();
        Destroy(gameObject, 0.05f);
    }

    void TriggerParticles() {
        GameObject particles = Instantiate(blockParticles, transform.position, transform.rotation);
    }

    void DispenseUpgrade() {
        if (hasUpgrade) {
            GameObject upgradeCan = Instantiate(upgradeContainer, transform.position, transform.rotation);
        }
    }

}