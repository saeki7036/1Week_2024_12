using System.Collections;
using System.Collections.Generic;
using TMPro;
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
        GameOver
    }
    [SerializeField]
    GameState gameState;
   
    public bool IsMainGameState => gameState == GameState.MainGame;
    public bool IsGameClear => gameState == GameState.Clear;
    public bool IsGameOver => gameState == GameState.GameOver;
    [SerializeField]
    Player playerScript;

    [SerializeField]
    GameObject finishAnimartionObject;

    [SerializeField] 
    TextMeshProUGUI scoreText;
    [SerializeField] 
    TextMeshProUGUI clearScoreText;
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
        scoreText.text = Score.ToString();
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
            return GameState.GameOver;

        if (BossDard)
        {
            clearScoreText.text = scoreText.text;
            return GameState.Clear;
        }
           

        return GameState.MainGame;
    }
}
