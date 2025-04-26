using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperAttackScreen : SuperAttackBase
{
    [SerializeField] 
    GameObject Black;//生成するオブジェクト

    [SerializeField] 
    int screenValue = 20;//スクリーン個数

    [SerializeField] 
    float interval = 0.03f;//遅延インターバル

    [SerializeField]
    float DestroyTime = 0.8f;//破壊時間

    [SerializeField]
    float minPosX = -8.5f;//x範囲最小値

    [SerializeField]
    float maxPosX = 0f;//x範囲最大値

    [SerializeField]
    float minPosY = -5f;//y範囲最小値

    [SerializeField]
    float maxPosY = 5f;//y範囲最大値

    //ランダムに特定範囲から取得
    Vector2 randomPos => new Vector2(
        UnityEngine.Random.Range(minPosX, maxPosX),
        UnityEngine.Random.Range(minPosY, maxPosY));
    
    //必殺技起動
    public override void PlaySuperAttack()
    {
        //UniTask非同期処理起動
        //通常攻撃のScreenをぶっ飛ばす処理
        TimeToAttack();
    }

    //必殺技の処理
    async void TimeToAttack()
    {
        //UniTask用トークン
        var token = this.GetCancellationTokenOnDestroy();

        //発生させるスクリーン個数分回す
        for (int i = 0; i < screenValue; i++)
        {
            //インターバル遅延
            await UniTask.Delay(TimeSpan.FromSeconds(interval), cancellationToken: token);

            //オブジェクト生成
            GameObject CL_Black = Instantiate(Black);

            //オブジェクト位置変更
            CL_Black.transform.position = randomPos;

            //Destroyで削除処理
            Destroy(CL_Black, DestroyTime);
        }
    }
}
