using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightBullet : BulletBase
{
    //�P�̃e�X�g�ς�

    //�e�X�g�p�ϐ�
    //Vector3 forcas = Vector3.zero;

    protected override void BulletSetUp()
    {
        //�����e�̂�
        Vector2 velocity = rb2D.velocity;

        //speed�𔽉f
        rb2D.velocity = velocity.normalized * speed;

        //�e�X�g�p����
        //forcas = (transform.position - vector).normalized;
    }

    /*
    void FixedUpdate()
    {
        //�e�X�g�p����
        //transform.Translate(forcas * speed);
    }
    */
}
