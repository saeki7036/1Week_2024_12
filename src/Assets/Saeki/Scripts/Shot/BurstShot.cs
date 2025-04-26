using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstShot : ShotPatarnBase
{
    [SerializeField] 
    GameObject bulletPrehab;//Bulletのオブジェクト

    [SerializeField]
    float patarnReportTimeSpan = 0.2f;//発射間隔の遅延時間

    [SerializeField]
    int patarnReportValue = 3;//発射パターンの起動回数

    //発射処理
    public override void PatarnPlay(Transform enemyTransform)
    {
        //発射対象の位置を取得
        Vector3 target = GameManager.Getplayer.transform.position;

        //UniTask非同期処理起動
        //発射処理の遅延処理の起動
        PatarnDelay(target, enemyTransform);
    }

    //発射処理の遅延処理
    async void PatarnDelay(Vector3 target, Transform enemyTransform)
    {
        //UniTask用トークン
        var token = this.GetCancellationTokenOnDestroy();

        //一定回数Delayかけた後に、発射パターンを起動
        for (int i = 0; i < patarnReportValue; i++)
        {
            //インターバル遅延
            await UniTask.Delay(TimeSpan.FromSeconds(patarnReportTimeSpan), cancellationToken: token);

            //複数回の発射処理起動
            RepeatPatarnPlay(target, enemyTransform);
        }
    }

    //複数回の発射処理
    void RepeatPatarnPlay(Vector3 target, Transform enemyTransform)
    {
        //nullチェック
        if (enemyTransform == null)
            return;

        //プレイヤーに飛ばす方向を計算
        Vector2 dirTarget = target - enemyTransform.position;

        //オブジェクト生成
        GameObject bullet = Instantiate(bulletPrehab, enemyTransform.position, Quaternion.identity);

        //Rigidbody2D取得
        Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();

        //ノーマライズ
        Vector2 rotate = dirTarget.normalized;

        //発射方向代入
        bulletRB.velocity = rotate;
    }
}
