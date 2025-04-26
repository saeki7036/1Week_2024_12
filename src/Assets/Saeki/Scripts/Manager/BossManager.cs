using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    [SerializeField]
    BossEnemyLeader bossEnemy;//ボスの本体

    [SerializeField]
    GameObject bossEnemyObject;//ボスの大元のオブジェクト

    [SerializeField]
    BossEnemySub[] bossSub;//ボスの腕

    [SerializeField]
    float downParameter = -11f;//画面外からの移動量

    [SerializeField]
    float moveTime = 5f;//移動時間

    // Start is called before the first frame update
    void Start()
    {
        //画面外から画面上部まで移動
        this.transform.DOMove(new Vector3(
            transform.position.x, //移動無し
            transform.position.y +downParameter, //y軸で移動
            0f), moveTime);//z軸は2Dのため0でOK
            
        //ボスの形態変化UniTask起動
        SetUpBossLeader();

        //ボスの死亡処理UniTask起動
        bossDeadCheck();
    }

    //ボスの形態変化(UniTask)
    async void SetUpBossLeader()
    {
        //UniTask用トークン
        var token = this.GetCancellationTokenOnDestroy();

        //全ての腕の破壊まで待機
        for (int i = 0; i < bossSub.Length;i++)//それぞれの腕のHPが0になったら処理の進行
        await UniTask.WaitUntil(() => bossSub[i].IsDestroyed(), cancellationToken: token);

        //本体の当たり判定の変更(あたるように)
        bossEnemyObject.layer = LayerMask.NameToLayer("Enemy");

        //形態変化
        bossEnemy.SetSuperMode();
    }

    //ボスの死亡処理(UniTask)
    async void bossDeadCheck()
    {
        //UniTask用トークン
        var token = this.GetCancellationTokenOnDestroy();

        //本体の死亡まで待機
        await UniTask.WaitUntil(() => bossEnemyObject.activeSelf == false, cancellationToken: token);

        //クリアフラグ設定
        GameClear();
    }

    //クリアフラグ設定
    void GameClear()
    {
        //GameManagerを取得
        GameManager manager = GameObject.FindWithTag("Manager").GetComponent<GameManager>();

        //GameManagerにフラグ送信
        if (manager != null)
            manager.BosskillFlag();
    }
}
