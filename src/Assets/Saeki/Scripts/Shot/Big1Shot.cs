using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class Big1Shot : ShotPatarnBase
{
    [SerializeField] GameObject bulletPrehab;
    public override void PatarnPlay(Transform enemyTransform)
    {
        Vector3 target = GameManager.Getplayer.transform.position;
        Vector2 dirTarget = target - enemyTransform.position;

        GameObject bullet = Instantiate(bulletPrehab, enemyTransform.position, Quaternion.identity);
        Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();

        Vector2 rotate = dirTarget.normalized;
        bulletRB.velocity = rotate;
    }
}
