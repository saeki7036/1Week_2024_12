using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotBigHorming : ShotPatarnBase
{
    [SerializeField] GameObject bulletSmall;
    [SerializeField] GameObject bulletLarge;
    [SerializeField] float aimValue = 10f;
    [SerializeField] float OtherPlayTimeSpan = 0.2f;
    [SerializeField] int OtherPlayCount = 5;
    public override void PatarnPlay(Transform enemyTransform)
    {
        Vector3 target = GameManager.Getplayer.transform.position;

        Vector2 dirTarget = target - enemyTransform.position;

        GameObject bullet = Instantiate(bulletLarge, enemyTransform.position, Quaternion.identity);
        Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();

        Vector2 rotate = Quaternion.Euler(Vector3.forward) * dirTarget.normalized;
        bulletRB.velocity = rotate;
        
        OtherPatarnDelay(target, enemyTransform);
    }

    async void OtherPatarnDelay(Vector3 target, Transform enemyTransform) 
    {
        var token = this.GetCancellationTokenOnDestroy();
        for (int i = 0;i < OtherPlayCount; i++)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(OtherPlayTimeSpan), cancellationToken: token);
            OtherPatarnPlay(target, enemyTransform);
        }
    }

    void OtherPatarnPlay(Vector3 target, Transform enemyTransform)
    {
        for (int i = 3; i <= 33; i++)
        {
            Vector2 dirTarget = target - enemyTransform.position;

            GameObject bullet = Instantiate(bulletSmall, enemyTransform.position, Quaternion.identity);
            Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();

            float angleRadians = (aimValue * i) * Mathf.Deg2Rad;
            Vector2 rotate = Quaternion.Euler(Vector3.forward * angleRadians) * dirTarget.normalized;

            bulletRB.velocity = rotate;
        }
    }
}
