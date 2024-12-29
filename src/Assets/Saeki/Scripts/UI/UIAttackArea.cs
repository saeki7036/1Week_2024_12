using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAttackArea : MonoBehaviour
{
    [SerializeField]
    float MoveValue = 10f;

    Player player;

    void Start()
    {
        if (player == null)
        {
            GameObject playerObject = GameObject.FindWithTag("Player");
            if (playerObject != null && playerObject.TryGetComponent<Player>(out var p))
                player = p;
        }
    }
    public void MoveArea()
    {
        this.gameObject.SetActive(true);

        this.transform.DOScaleX(MoveValue, 2f).SetLoops(2, LoopType.Yoyo).SetDelay(0.5f).OnComplete(() =>
        {
            this.gameObject.SetActive(false);
            Destroy(this.gameObject);
        });
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<BulletBase>(out BulletBase bullet))
        {
            bullet.KillBullet();
            player.AddPoint(5);
            return;
        }
        else if (collision.TryGetComponent<EnemyBase>(out EnemyBase enemy))
        {
            enemy.TakeDamage(200f);
            player.AddPoint(10);
        }

    }
}
