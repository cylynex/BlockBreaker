using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

    public void LoadNextScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadSpecificScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }
    
    public void StartNewGame() {
        //GameObject.FindObjectOfType<ScoreKeeper>().ResetScore();
        ScoreKeeper.currentPoints = 0;
        SceneManager.LoadScene("StartMenu");
    }

    public void QuitGame() {
        Application.Quit();
    }


}