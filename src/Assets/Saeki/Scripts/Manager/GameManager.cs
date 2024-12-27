using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //staticパラメータを使用
    static GameObject playerObject;

    public static GameObject Getplayer => playerObject;

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
        playerObject = GameObject.FindGameObjectWithTag("Player");
        gameState = GameState.MainGame;
    }

    
    void FixedUpdate()
    {
        
    }
}
