using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScreen : MonoBehaviour
{
    [SerializeField]
    Player player;

    [SerializeField]
    float takeDamageValue = 100f;//敵に与えるダメージ量

    void Start()
    {
        //playerがnullなら探索を行う。(PlayerのTagはSceneに一つだけなので検索量O(1)。)
        if(player == null)
        {
            GameObject playerObject = GameObject.FindWithTag("Player");

            //PlayerObjectにあるplayerクラスをTryGetで代入(基本成功する)
            if (playerObject != null && playerObject.TryGetComponent<Player>(out var p))
                player = p;
        }
    }

    //攻撃判定処理
    void OnTriggerEnter2D(Collider2D collision)
    {
        //playerがnullなら止める
        if (player == null) return;

        //プレイヤー攻撃側の当たり判定処理
        //Bulletの基底クラスを取得
        if (collision.TryGetComponent<BulletBase>(out BulletBase bullet))
        {
            //必殺技ポイント貯める
            player.AddPoint(bullet.GetAddPoint);

            //Bulletのスケール変更
            bullet.SetScale();
            return;
        }
        //Enemyの基底クラスを取得
        else if (collision.TryGetComponent<EnemyBase>(out EnemyBase enemy))
        {
            //必殺技ポイント貯める
            player.AddPoint(enemy.GetAddPoint);

            //Enemyのダメージ処理
            enemy.TakeDamage(takeDamageValue);
            return;
        }
    }
}
