using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using unityroom.Api;

public class GameManager : MonoBehaviour
{
    //staticパラメータを使用
    //プレイヤーのオブジェクト情報をキャッシュ
    static GameObject playerObject;

    //EnemyやBulletの計算に使用
    public static GameObject Getplayer => playerObject;

    //staticパラメータを使用
    //スコア管理変数
    static int mainGameScore;
   
    //スコア加算
    public static void AddScore(int add) => mainGameScore += add;

    enum GameState//ゲームステート
    {
        Before,
        MainGame,
        Clear,
        GameOver
    }

    [SerializeField]
    GameState gameState;//ゲームステート管理変数

    public bool IsMainGameState => gameState == GameState.MainGame;//メインゲームのフラグ判定

    public bool IsGameOver => gameState == GameState.GameOver;//ゲームオーバーのフラグ判定

     bool IsGameClear => gameState == GameState.Clear;//クリアのフラグ判定

    [SerializeField]
    Player playerScript;//プレイヤーのクラス

    [SerializeField]
    GameObject finishAnimartionObject;//クリア時のアニメーションのオブジェクト

    [SerializeField] 
    TextMeshProUGUI scoreText;//スコア表示テキスト

    [SerializeField] 
    TextMeshProUGUI clearScoreText;//クリア時のスコア表示テキスト

    //trueに設定するとUnityRoomのランキング機能を利用
    [SerializeField] 
    bool sendScoreFlag = false;

    bool bossDead;//ボスの死亡フラグ

    public void BosskillFlag() => bossDead = true; //ボスの死亡フラグ更新

    void Start()
    {
        //スコア初期化
        mainGameScore = 0;

        //Playerをキャッシュ
        playerObject = GameObject.FindGameObjectWithTag("Player");

        //ゲームステート初期化
        gameState = GameState.MainGame;

        //ボスの死亡フラグ初期化
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
            PlayFinishAnimation();//アニメーション起動
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

            //クリア状態
            return GameState.Clear;
        }

        //それ以外はメインゲーム状態
        return GameState.MainGame;
    }
}
