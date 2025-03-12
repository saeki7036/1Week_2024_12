using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HormingBullet : BulletBase
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

        //�C���^�[�o���x��
        await UniTask.Delay(TimeSpan.FromSeconds(Interval), cancellationToken: token);

        //�v���C���[�����ɕύX
        Vector2 velocityFromPlayer = GameManager.Getplayer.transform.position - transform.position;
        rb2D.velocity = velocityFromPlayer.normalized * speed;
    }
}
