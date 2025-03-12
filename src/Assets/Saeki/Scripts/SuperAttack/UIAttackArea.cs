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
        //player��null�Ȃ�T�����s��(Player��Tag��Scene�Ɉ�����Ȃ̂�O(1)�B)
        if (player == null)
        {
            GameObject playerObject = GameObject.FindWithTag("Player");
            if (playerObject != null && playerObject.TryGetComponent<Player>(out var p))
                player = p;
        }
    }
    public void MoveArea()
    {
        //���̃I�u�W�F�N�g���A�N�e�B�u��Ԃ�
        this.gameObject.SetActive(true);

        //DOTween���g����X�̃X�P�[����ό`
        this.transform.DOScaleX(moveValue, 2f).SetLoops(2, LoopType.Yoyo).SetDelay(0.5f).OnComplete(() =>
        {
            //�������A�N�e�B�u��Ԃ�
            this.gameObject.SetActive(false);

            //�I�u�W�F�N�g�폜
            Destroy(this.gameObject);
        });
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (player == null) return;

        //�����蔻��
        if (collision.TryGetComponent<BulletBase>(out BulletBase bullet))
        {
            //�K�E�Z���߂�
            player.AddPoint(5);
            //�e�̍폜���s��
            bullet.KillBullet();
            
            return;
        }
        else if (collision.TryGetComponent<EnemyBase>(out EnemyBase enemy))
        {
            //�K�E�Z���߂�
            player.AddPoint(10);
            //�_���[�W����
            enemy.TakeDamage(200f);

            return;
        }
    }
}
