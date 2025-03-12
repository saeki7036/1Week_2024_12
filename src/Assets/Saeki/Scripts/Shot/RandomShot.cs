using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class RandomShot : ShotPatarnBase
{
    [SerializeField] 
    GameObject bulletSmall;

    [SerializeField] 
    float aimValue = 560f;

    [SerializeField] 
    float patarnReportTimeSpan = 0.1f;

    [SerializeField]
    int patarnReportValue = 50;
    
    [SerializeField]
    int oneSetBulletCount = 3;

    public override void PatarnPlay(Transform enemyTransform)
    {
        OtherPatarnDelay(enemyTransform);
    }

    async void OtherPatarnDelay(Transform enemyTransform)
    {
        var token = this.GetCancellationTokenOnDestroy();

        //一定回数Delayかけた後に、発射パターンを起動
        for (int i = 0; i < patarnReportValue; i++)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(patarnReportTimeSpan), cancellationToken: token);
            OtherPatarnPlay(enemyTransform);
        }
    }

    void OtherPatarnPlay(Transform enemyTransform)
    {
        if (GameManager.Getplayer == null) 
            return;

        if (enemyTransform == null)
            return;

        Vector3 target = GameManager.Getplayer.transform.position;

        //発射回数分ループ
        for (int i = 0; i < oneSetBulletCount; i++)
        {
            //基準となる方向を計算
            Vector2 dirTarget = target - enemyTransform.position;

            //オブジェクト生成
            GameObject bullet = Instantiate(bulletSmall, enemyTransform.position, Quaternion.identity);

            //Rigidbody2D取得
            Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();

            //ランダムに方向を指定
            float angleRadians = (aimValue * UnityEngine.Random.Range(0,37)) * Mathf.Deg2Rad;

            //発射方向計算
            Vector2 rotate = Quaternion.Euler(Vector3.forward * angleRadians) * dirTarget.normalized;

            //発射方向代入
            bulletRB.velocity = rotate;
        }
    }
}
