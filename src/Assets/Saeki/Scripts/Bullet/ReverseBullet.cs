using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseBullet : BulletBase
{
    [SerializeField]
    float Interval = 3f;//�x������
    protected override void BulletSetUp()
    {
        //velocity���擾
        Vector2 velocity = rb2D.velocity;

        //velocity��speed�𔽉f
        rb2D.velocity = velocity.normalized * speed;

        //UniTask�񓯊������N��
        Reverse();
    }

    async void Reverse()
    {
        //UniTask�p�g�[�N��
        var token = this.GetCancellationTokenOnDestroy();

        //�C���^�[�o���x��
        await UniTask.Delay(TimeSpan.FromSeconds(Interval), cancellationToken: token);

        //���݂Ƌt�̕������v�Z
        Vector2 velocityFromPlayer = (rb2D.velocity)* -1;

        //���݂Ƃ͋t�����ɕύX
        rb2D.velocity = velocityFromPlayer.normalized * speed;
    }
}
