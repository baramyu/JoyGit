using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    waiting,
    inGame
}
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameState gameState;


    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void GameStart()
    {

        gameState = GameState.inGame;
    }
    public void GameOver()
    {
        gameState = GameState.waiting;
    }

}
