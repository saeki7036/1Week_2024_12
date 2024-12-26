using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    enum GameState
    {
        Before,
        MainGame,
        Clear,
        GameOvar
    }

    GameState gameState;

    public bool IsMainGameState => gameState == GameState.MainGame;
    // Start is called before the first frame update
    void Start()
    {
        gameState = GameState.MainGame;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
}
