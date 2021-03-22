using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject gameOver;

    [Header("Text")]
    [SerializeField] private Text gameOverText;

    //Gameplay HUD functions to restart game or go back to menu
    public void GameOver(bool win)
    {
        Time.timeScale = 0.0f;
        gameOver.SetActive(true);
        if (win)
            gameOverText.text = "You Win!";
        else
            gameOverText.text = "You Lose!";
    }

    public void Restart()
    {
        LevelManager.Instance.LoadLevel(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        LevelManager.Instance.LoadLevel("MainMenu");
    }
}
