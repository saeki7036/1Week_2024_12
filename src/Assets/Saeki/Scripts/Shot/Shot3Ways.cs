using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot3Ways : ShotPatarnBase
{
    [SerializeField] 
    GameObject bulletPrehab;//Bullet�̃I�u�W�F�N�g

    [SerializeField] 
    float aimValue = 1000f;//�����v�Z���x

    //3Way�̂��߁A�␳���A-1,0,1�̐��l�ł�����
    //-1�`1�̊Ԃ��w�肵��3�������w�肷�邽�߂̐��l
    const int MinForwardValue = -1,
              MaxForwardValue = 1;

    //���ˏ���
    public override void PatarnPlay(Transform enemyTransform)
    {
        //null�`�F�b�N
        if (enemyTransform == null)
            return;

        //���ˑΏۂ̈ʒu���擾
        Vector3 target = GameManager.Getplayer.transform.position;

        //3Way�̂��߁A�␳���A-1,0,1�̐��l�ł�����
        for (int i = MinForwardValue; i <= MaxForwardValue; i++)
        {
            //��ƂȂ�������v�Z
            Vector2 dirTarget =  target - enemyTransform.position;

            //�I�u�W�F�N�g����
            GameObject bullet = Instantiate(bulletPrehab, enemyTransform.position, Quaternion.identity);

            //Rigidbody2D�擾
            Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();

            //���˕����v�Z
            float angleRadians = (aimValue * i)* Mathf.Deg2Rad;

            //���˕����v�Z
            Vector2 rotate = Quaternion.Euler(Vector3.forward * angleRadians) * dirTarget.normalized;

            //���˕������
            bulletRB.velocity = rotate;
        }
    }
}
