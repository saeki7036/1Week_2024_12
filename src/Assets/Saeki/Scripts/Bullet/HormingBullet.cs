using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HormingBullet : BulletBase
{
    [SerializeField]
    float Interval = 3f;//遅延時間

    protected override void BulletSetUp()
    {
        //velocityを取得
        Vector2 velocity = rb2D.velocity;

        //velocityにspeedを反映
        rb2D.velocity = velocity.normalized * speed;

        //UniTask非同期処理起動
        Horming();
    }

    async void Horming()
    {
        //UniTask用トークン
        var token = this.GetCancellationTokenOnDestroy();

        //インターバル遅延
        await UniTask.Delay(TimeSpan.FromSeconds(Interval), cancellationToken: token);

        //プレイヤー方向を計算
        Vector2 velocityFromPlayer = GameManager.Getplayer.transform.position - transform.position;

        //プレイヤー方向に変更
        rb2D.velocity = velocityFromPlayer.normalized * speed;
    }
}
