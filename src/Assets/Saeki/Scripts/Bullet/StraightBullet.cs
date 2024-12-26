using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightBullet : BulletBase
{
    Vector3 forcas = Vector3.zero;
    async void Start()
    {
        Vector2 velocity = rb2D.velocity;
        rb2D.velocity = velocity.normalized * speed;
        await UniTask.Yield();
        //forcas = (transform.position - vector).normalized;
    }


    void FixedUpdate()
    {
        //transform.Translate(forcas * speed);
    }
}
