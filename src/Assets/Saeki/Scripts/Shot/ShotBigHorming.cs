using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class ShotBigHorming : ShotPatarnBase
{
    [SerializeField] 
    GameObject bulletSmall;
    [SerializeField] 
    GameObject bulletLarge;

    [SerializeField] 
    float aimValue = 10f;

    [SerializeField] 
    float patarnReportTimeSpan = 0.2f;

    [SerializeField] 
    int patarnReportValue = 5;

    public override void PatarnPlay(Transform enemyTransform)
    {
        Vector3 target = GameManager.Getplayer.transform.position;

        //相手に向かって直線に発射するパターン
        MainPatarnPlay(target, enemyTransform);
        //相手以外の方向に扇形に発射するパターン
        OtherPatarnDelay(target, enemyTransform);
    }

    void MainPatarnPlay(Vector3 target, Transform enemyTransform)
    {
        //プレイヤーに飛ばす方向を計算
        Vector2 dirTarget = target - enemyTransform.position;

        //オブジェクト生成
        GameObject bullet = Instantiate(bulletLarge, enemyTransform.position, Quaternion.identity);

        //Rigidbody2D取得
        Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();

        //ノーマライズ
        Vector2 rotate = Quaternion.Euler(Vector3.forward) * dirTarget.normalized;

        //発射方向代入
        bulletRB.velocity = rotate;
    }



    async void OtherPatarnDelay(Vector3 target, Transform enemyTransform) 
    {
        var token = this.GetCancellationTokenOnDestroy();

        //一定回数Delayかけた後に、発射パターンを起動
        for (int i = 0;i < patarnReportValue; i++)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(patarnReportTimeSpan), cancellationToken: token);
            OtherPatarnPlay(target, enemyTransform);
        }
    }

    void OtherPatarnPlay(Vector3 target, Transform enemyTransform)
    {
        //プレイヤーの方向以外の方向にばら撒くため3～33を指定
        for (int i = 3; i <= 33; i++)
        {
            if (enemyTransform == null)
                break;

            //基準となる方向を計算
            Vector2 dirTarget = target - enemyTransform.position;

            //オブジェクト生成
            GameObject bullet = Instantiate(bulletSmall, enemyTransform.position, Quaternion.identity);

            //Rigidbody2D取得
            Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();

            //発射方向計算
            float angleRadians = (aimValue * i) * Mathf.Deg2Rad;
            Vector2 rotate = Quaternion.Euler(Vector3.forward * angleRadians) * dirTarget.normalized;

            //発射方向代入
            bulletRB.velocity = rotate;
        }
    }
}
