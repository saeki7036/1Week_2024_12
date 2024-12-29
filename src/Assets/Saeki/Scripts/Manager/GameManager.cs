using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    //staticパラメータを使用
    static GameObject playerObject;

    public static GameObject Getplayer => playerObject;

    static int Score;
    public int GetScore => Score;
    public static void AddScore(int add) => Score += add;
    enum GameState
    {
        Before,
        MainGame,
        Clear,
        GameOvar
    }

    GameState gameState;
   
    public bool IsMainGameState => gameState == GameState.MainGame;
    
    void Start()
    {
        Score = 0;
        playerObject = GameObject.FindGameObjectWithTag("Player");
        gameState = GameState.MainGame;
    }

    
    void FixedUpdate()
    {
        
    }
}
