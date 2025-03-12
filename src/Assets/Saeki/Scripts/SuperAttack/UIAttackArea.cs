using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAttackArea : MonoBehaviour
{
    [SerializeField]
    float moveValue = 10f;

    Player player;

    void Start()
    {
        //playerがnullなら探索を行う(PlayerのTagはSceneに一つだけなのでO(1)。)
        if (player == null)
        {
            GameObject playerObject = GameObject.FindWithTag("Player");
            if (playerObject != null && playerObject.TryGetComponent<Player>(out var p))
                player = p;
        }
    }
    public void MoveArea()
    {
        //このオブジェクトをアクティブ状態に
        this.gameObject.SetActive(true);

        //DOTweenを使ってXのスケールを変形
        this.transform.DOScaleX(moveValue, 2f).SetLoops(2, LoopType.Yoyo).SetDelay(0.5f).OnComplete(() =>
        {
            //完了後非アクティブ状態に
            this.gameObject.SetActive(false);

            //オブジェクト削除
            Destroy(this.gameObject);
        });
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (player == null) return;

        //当たり判定
        if (collision.TryGetComponent<BulletBase>(out BulletBase bullet))
        {
            //必殺技貯める
            player.AddPoint(5);
            //弾の削除を行う
            bullet.KillBullet();
            
            return;
        }
        else if (collision.TryGetComponent<EnemyBase>(out EnemyBase enemy))
        {
            //必殺技貯める
            player.AddPoint(10);
            //ダメージ処理
            enemy.TakeDamage(200f);

            return;
        }
    }
}
