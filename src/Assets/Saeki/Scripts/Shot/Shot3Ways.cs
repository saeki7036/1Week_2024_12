using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shot3Ways : ShotPatarnBase
{
    [SerializeField] 
    GameObject bulletPrehab;

    [SerializeField] 
    float aimValue = 15f;

    public override void PatarnPlay(Transform enemyTransform)
    {
        if (enemyTransform == null)
            return;

        Vector3 target = GameManager.Getplayer.transform.position;

        //3Way�̂��߁A�␳���A-1,0,1�̐��l�ł�����
        for (int i = -1; i <= 1; i++)
        {
            //��ƂȂ�������v�Z
            Vector2 dirTarget =  target - enemyTransform.position;

            //�I�u�W�F�N�g����
            GameObject bullet = Instantiate(bulletPrehab, enemyTransform.position, Quaternion.identity);

            //Rigidbody2D�擾
            Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();

            //���˕����v�Z
            float angleRadians = (aimValue * i)* Mathf.Deg2Rad;
            Vector2 rotate = Quaternion.Euler(Vector3.forward * angleRadians) * dirTarget.normalized;

            //���˕������
            bulletRB.velocity = rotate;
        }
    }
}
