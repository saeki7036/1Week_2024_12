using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomShot : ShotPatarnBase
{
    [SerializeField] GameObject bulletSmall;
    [SerializeField] float aimValue = 560f;
    [SerializeField] float OtherPlayTimeSpan = 0.1f;
    [SerializeField] int OtherPlayCount = 30;
    [SerializeField] int OneSetBulletCount = 3;
    public override void PatarnPlay(Transform enemyTransform)
    {
        OtherPatarnDelay(enemyTransform);
    }

    async void OtherPatarnDelay(Transform enemyTransform)
    {
        var token = this.GetCancellationTokenOnDestroy();
        for (int i = 0; i < OtherPlayCount; i++)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(OtherPlayTimeSpan), cancellationToken: token);
            OtherPatarnPlay(enemyTransform);
        }
    }

    void OtherPatarnPlay(Transform enemyTransform)
    {
        if (GameManager.Getplayer == null) return;

        Vector3 target = GameManager.Getplayer.transform.position;

        for (int i = 0; i < OneSetBulletCount; i++)
        {
            if (enemyTransform == null)
                break;

            Vector2 dirTarget = target - enemyTransform.position;

            GameObject bullet = Instantiate(bulletSmall, enemyTransform.position, Quaternion.identity);
            Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();

            float angleRadians = (aimValue * UnityEngine.Random.Range(0,37)) * Mathf.Deg2Rad;
            Vector2 rotate = Quaternion.Euler(Vector3.forward * angleRadians) * dirTarget.normalized;

            bulletRB.velocity = rotate;
        }
    }
}
