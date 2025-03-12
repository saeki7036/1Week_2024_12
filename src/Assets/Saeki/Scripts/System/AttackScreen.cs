using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScreen : MonoBehaviour
{
    [SerializeField]
    Player player;

    void Start()
    {
        //player��null�Ȃ�T�����s��(Player��Tag��Scene�Ɉ�����Ȃ̂�O(1)�B)
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

        //�����蔻��
        if(collision.TryGetComponent<BulletBase>(out BulletBase bullet))
        {
            //�K�E�Z���߂�
            player.AddPoint(5);
            //�X�P�[���ύX
            bullet.SetScale();
            return;
        }
        else if (collision.TryGetComponent<EnemyBase>(out EnemyBase enemy))
        {
            //�K�E�Z���߂�
            player.AddPoint(10);
            //�_���[�W����
            enemy.TakeDamage(100f);
            return;
        }
    }
}
