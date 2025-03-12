using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScreen : MonoBehaviour
{
    [SerializeField]
    Player player;

    void Start()
    {
        //playerがnullなら探索を行う(PlayerのTagはSceneに一つだけなのでO(1)。)
        if(player == null)
        {
            GameObject playerObject = GameObject.FindWithTag("Player");
            if (playerObject != null && playerObject.TryGetComponent<Player>(out var p))
                player = p;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (player == null) return;

        //当たり判定
        if(collision.TryGetComponent<BulletBase>(out BulletBase bullet))
        {
            //必殺技貯める
            player.AddPoint(5);
            //スケール変更
            bullet.SetScale();
            return;
        }
        else if (collision.TryGetComponent<EnemyBase>(out EnemyBase enemy))
        {
            //必殺技貯める
            player.AddPoint(10);
            //ダメージ処理
            enemy.TakeDamage(100f);
            return;
        }
    }
}
