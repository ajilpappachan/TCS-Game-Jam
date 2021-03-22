using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private PlayerCharacter _player;
    private List<AICharacter> enemies;
    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<PlayerCharacter>();
        enemies = new List<AICharacter>();
        foreach(AICharacter ai in FindObjectsOfType<AICharacter>())
        {
            enemies.Add(ai);
        }
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
    }

    //Get input for player movement
    private void GetInput()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            _player.TurnLeft();
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            _player.TurnRight();
        }
    }

    //Check if the player is dead or if there are no enemies left and call Game Win or Lose function
    public void CheckWin(Character character)
    {
        if(character is PlayerCharacter)
        {
            GameLose();
        }
        else
        {
            enemies.Remove((AICharacter)character);
            if(enemies.Count == 0)
                GameWin();
        }
    }

    private void GameLose()
    {
        FindObjectOfType<UIManager>().GameOver(false);
    }

    private void GameWin()
    {
        FindObjectOfType<UIManager>().GameOver(true);
    }
}
