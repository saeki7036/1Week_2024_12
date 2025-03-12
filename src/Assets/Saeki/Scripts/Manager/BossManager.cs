using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    [SerializeField]
    BossEnemyLeader bossEnemy;

    [SerializeField]
    GameObject bossEnemyObject;

    [SerializeField]
    BossEnemySub[] bossSub;

    [SerializeField]
    float downParameter = -11f;
    // Start is called before the first frame update
    void Start()
    {
        //画面上部まで移動
        this.transform.DOMove(new Vector3(transform.position.x, transform.position.y +downParameter, 0f), 5f);
        SetUpBossLeader();
        bossDeadCheck();
    }

    async void SetUpBossLeader()
    {
        var token = this.GetCancellationTokenOnDestroy();
        //全ての腕の破壊まで待機
        for (int i = 0; i < bossSub.Length;i++)
        await UniTask.WaitUntil(() => bossSub[i].IsDestroyed(), cancellationToken: token);

        //本体の当たり判定の変更(あたるように)
        bossEnemyObject.layer = LayerMask.NameToLayer("Enemy");
        //形態変化
        bossEnemy.SetSuperMode();
    }

    async void bossDeadCheck()
    {
        var token = this.GetCancellationTokenOnDestroy();
        //本体の死亡まで待機
        await UniTask.WaitUntil(() => bossEnemyObject.activeSelf == false, cancellationToken: token);
        //クリアフラグ設定
        GameClear();
    }
    
    void GameClear()
    {
        //GameManagerにフラグ送信
        GameManager manager = GameObject.FindWithTag("Manager").GetComponent<GameManager>();
        if (manager != null)
            manager.BosskillFlag();
    }
}
