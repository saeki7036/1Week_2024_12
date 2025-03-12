using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseBullet : BulletBase
{
    [SerializeField]
    float Interval = 3f;
    protected override void BulletSetUp()
    {
        Vector2 velocity = rb2D.velocity;
        rb2D.velocity = velocity.normalized * speed;

        Horming();
    }

    async void Horming()
    {
        var token = this.GetCancellationTokenOnDestroy();

        //インターバル遅延
        await UniTask.Delay(TimeSpan.FromSeconds(Interval), cancellationToken: token);

        //現在とは逆方向に変更
        Vector2 velocityFromPlayer = (rb2D.velocity)* -1;
        rb2D.velocity = velocityFromPlayer.normalized * speed;
    }
}
