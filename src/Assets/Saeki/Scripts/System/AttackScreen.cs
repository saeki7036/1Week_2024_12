using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScreen : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<BulletBase>(out BulletBase bullet))
        {
            bullet.SetScale();
            return;
        }
        else if (collision.TryGetComponent<EnemyBase>(out EnemyBase enemy))
        {
            enemy.TakeDamage(100f);        
        }
    }
}
