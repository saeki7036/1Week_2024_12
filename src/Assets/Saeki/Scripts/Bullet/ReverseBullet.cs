using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseBullet : BulletBase
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
        Reverse();
    }

    async void Reverse()
    {
        //UniTask用トークン
        var token = this.GetCancellationTokenOnDestroy();

        //インターバル遅延
        await UniTask.Delay(TimeSpan.FromSeconds(Interval), cancellationToken: token);

        //現在と逆の方向を計算
        Vector2 velocityFromPlayer = (rb2D.velocity)* -1;

        //現在とは逆方向に変更
        rb2D.velocity = velocityFromPlayer.normalized * speed;
    }
}
