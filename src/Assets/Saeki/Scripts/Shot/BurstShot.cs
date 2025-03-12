using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BurstShot : ShotPatarnBase
{
    [SerializeField] 
    GameObject bulletPrehab;

    [SerializeField]
    float patarnReportTimeSpan = 0.2f;

    [SerializeField]
    int patarnReportValue = 3;

    public override void PatarnPlay(Transform enemyTransform)
    {
        Vector3 target = GameManager.Getplayer.transform.position;
        OtherPatarnDelay(target, enemyTransform);
    }

    async void OtherPatarnDelay(Vector3 target, Transform enemyTransform)
    {
        var token = this.GetCancellationTokenOnDestroy();

        //一定回数Delayかけた後に、発射パターンを起動
        for (int i = 0; i < patarnReportValue; i++)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(patarnReportTimeSpan), cancellationToken: token);
            OtherPatarnPlay(target, enemyTransform);
        }
    }

    void OtherPatarnPlay(Vector3 target, Transform enemyTransform)
    {
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
