using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightBullet : BulletBase
{
    //�P�̃e�X�g�ς�

    //Vector3 forcas = Vector3.zero;
    protected override void BulletSetUp()
    {
        //�����e�̂�
        Vector2 velocity = rb2D.velocity;
        rb2D.velocity = velocity.normalized * speed;
        //forcas = (transform.position - vector).normalized;
    }


    void FixedUpdate()
    {
        //transform.Translate(forcas * speed);
    }
}
