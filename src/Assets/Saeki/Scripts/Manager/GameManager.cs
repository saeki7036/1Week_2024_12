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
    [SerializeField]
    GameState gameState;
   
    public bool IsMainGameState => gameState == GameState.MainGame;
    public bool IsGameClear => gameState == GameState.Clear;

    [SerializeField]
    Player playerScript;

    [SerializeField]
    GameObject finishAnimartionObject;

    bool BossDard;

    public void BosskillFlag() => BossDard = true; 

    void Start()
    {
        Score = 0;
        playerObject = GameObject.FindGameObjectWithTag("Player");
        gameState = GameState.MainGame;
        BossDard = false;
    }

     
    
    void FixedUpdate()
    {
        gameState = stateChange();
        if (IsGameClear) PlayFinishAnimation();
    }

    void PlayFinishAnimation()
    {
        if(finishAnimartionObject.activeSelf == false)
            finishAnimartionObject.SetActive(true);
    }

    GameState stateChange()
    {
        if (gameState != GameState.MainGame)
            return gameState;

        if(playerScript.IsDard)
            return GameState.GameOvar;

        if(BossDard)
            return GameState.Clear;

        return GameState.MainGame;
    }
}
