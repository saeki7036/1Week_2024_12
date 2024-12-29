using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScreen : MonoBehaviour
{
    [SerializeField]
    Player player;

    void Start()
    {
        if(player == null)
        {
            GameObject playerObject = GameObject.FindWithTag("Player");
            if (playerObject != null && playerObject.TryGetComponent<Player>(out var p))
                player = p;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<BulletBase>(out BulletBase bullet))
        {
            bullet.SetScale();
            player.AddPoint(5);
            return;
        }
        else if (collision.TryGetComponent<EnemyBase>(out EnemyBase enemy))
        {
            player.AddPoint(10);
            enemy.TakeDamage(100f);        
        }
    }
}
