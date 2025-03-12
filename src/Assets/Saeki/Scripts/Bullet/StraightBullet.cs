using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightBullet : BulletBase
{
    //単体テスト済み

    //Vector3 forcas = Vector3.zero;
    protected override void BulletSetUp()
    {
        //直線弾のみ
        Vector2 velocity = rb2D.velocity;
        rb2D.velocity = velocity.normalized * speed;
        //forcas = (transform.position - vector).normalized;
    }


    void FixedUpdate()
    {
        //transform.Translate(forcas * speed);
    }
}
