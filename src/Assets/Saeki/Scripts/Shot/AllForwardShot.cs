using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllForwardShot : ShotPatarnBase
{
    [SerializeField] GameObject bulletSmall;
    [SerializeField] float aimValue = 10f;
    [SerializeField] float OtherPlayTimeSpan = 0.3f;
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
        for (int i = 0; i <= 36; i++)
        {
            if (enemyTransform == null)
                break;

            Vector2 dirTarget = target - enemyTransform.position;

            GameObject bullet = Instantiate(bulletSmall, enemyTransform.position, Quaternion.identity);
            Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();

            float angleRadians = (aimValue * i) * Mathf.Deg2Rad;
            Vector2 rotate = Quaternion.Euler(Vector3.forward * angleRadians) * dirTarget.normalized;

            bulletRB.velocity = rotate;
        }
    }
}
