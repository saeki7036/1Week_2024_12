using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using unityroom.Api;
public class GameManager : MonoBehaviour
{
    //staticパラメータを使用
    static GameObject playerObject;

    //プレイヤーのオブジェクト情報をキャッシュ
    public static GameObject Getplayer => playerObject;

    static int mainGameScore;
    public int GetScore => mainGameScore;
    public static void AddScore(int add) => mainGameScore += add;
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

    bool bossDead;

    //trueに設定するとUnityRoomのランキング機能を利用
    [SerializeField] 
    bool sendScoreFlag = false;

    public void BosskillFlag() => bossDead = true; 

    void Start()
    {
        mainGameScore = 0;
        playerObject = GameObject.FindGameObjectWithTag("Player");
        gameState = GameState.MainGame;
        bossDead = false;
    }

     
    
    void FixedUpdate()
    {
        //スコアText化
        scoreText.text = mainGameScore.ToString();

        //ゲームフロー変更
        gameState = stateChange();

        //ゲームクリア時
        if (IsGameClear) 
            PlayFinishAnimation();
    }

    void PlayFinishAnimation()
    {
        //クリア時アニメーションのオブジェクトのアクティブ状態を変更
        if(finishAnimartionObject.activeSelf == false)
            finishAnimartionObject.SetActive(true);
    }

    GameState stateChange()
    {
        //メインゲーム以外なら現在のゲームフローを維持
        if (gameState != GameState.MainGame)
            return gameState;

        //プレイヤー死亡
        if(playerScript.IsDard)
            return GameState.GameOver;

        //ボス死亡
        if (bossDead)
        {
            //クリア時のスコアで更新
            clearScoreText.text = mainGameScore.ToString();

            //UnityroomのScore送信用(ハイスコア順)
            if(sendScoreFlag)
                UnityroomApiClient.Instance.SendScore(1, mainGameScore, ScoreboardWriteMode.HighScoreDesc);

            return GameState.Clear;
        }
           
        return GameState.MainGame;
    }
}
