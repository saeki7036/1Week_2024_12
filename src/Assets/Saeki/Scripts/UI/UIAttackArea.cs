using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAttackArea : MonoBehaviour
{
    [SerializeField]
    float MoveValue = 10f;

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
            return;
        }
        else if (collision.TryGetComponent<EnemyBase>(out EnemyBase enemy))
        {
            enemy.TakeDamage(200f);
        }
    }
}
