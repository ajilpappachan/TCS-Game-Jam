using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject howToPanel;

    //Main Menu Functions
    public void Play()
    {
        LevelManager.Instance.LoadLevel("Game");
    }

    public void HowTo(bool show)
    {
        mainPanel.SetActive(!show);
        howToPanel.SetActive(show);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
