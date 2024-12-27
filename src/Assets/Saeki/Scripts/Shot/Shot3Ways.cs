using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shot3Ways : ShotPatarnBase
{
    [SerializeField] GameObject bulletPrehab;
    [SerializeField] float aimValue = 15f;
    public override void PatarnPlay(Transform enemyTransform)
    {
        Vector3 target = GameManager.Getplayer.transform.position;

        for (int i = -1; i <= 1; i++)
        {

            Vector2 dirTarget =  target - enemyTransform.position;

            GameObject bullet = Instantiate(bulletPrehab, enemyTransform.position, Quaternion.identity);
            Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();

            float angleRadians = (aimValue * i)* Mathf.Deg2Rad;
            Vector2 rotate = Quaternion.Euler(Vector3.forward * angleRadians) * dirTarget.normalized;

            bulletRB.velocity = rotate;
        }
    }
}
