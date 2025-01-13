using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstShot : ShotPatarnBase
{
    [SerializeField] GameObject bulletPrehab;
    [SerializeField] float OtherPlayTimeSpan = 0.2f;
    [SerializeField] int OtherPlayCount = 3;
    public override void PatarnPlay(Transform enemyTransform)
    {
        Vector3 target = GameManager.Getplayer.transform.position;
        OtherPatarnDelay(target, enemyTransform);
    }

    async void OtherPatarnDelay(Vector3 target, Transform enemyTransform)
    {
        var token = this.GetCancellationTokenOnDestroy();
        for (int i = 0; i < OtherPlayCount; i++)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(OtherPlayTimeSpan), cancellationToken: token);
            OtherPatarnPlay(target, enemyTransform);
        }
    }

    void OtherPatarnPlay(Vector3 target, Transform enemyTransform)
    {
        if (enemyTransform == null)
            return;

        Vector2 dirTarget = target - enemyTransform.position;

        GameObject bullet = Instantiate(bulletPrehab, enemyTransform.position, Quaternion.identity);
        Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();

        Vector2 rotate = dirTarget.normalized;

        bulletRB.velocity = rotate;

    }
}
