using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScreen : MonoBehaviour
{
    [SerializeField]
    Player player;

    [SerializeField]
    float takeDamageValue = 100f;//�G�ɗ^����_���[�W��

    void Start()
    {
        //player��null�Ȃ�T�����s���B(Player��Tag��Scene�Ɉ�����Ȃ̂Ō�����O(1)�B)
        if(player == null)
        {
            GameObject playerObject = GameObject.FindWithTag("Player");

            //PlayerObject�ɂ���player�N���X��TryGet�ő��(��{��������)
            if (playerObject != null && playerObject.TryGetComponent<Player>(out var p))
                player = p;
        }
    }

    //�U�����菈��
    void OnTriggerEnter2D(Collider2D collision)
    {
        //player��null�Ȃ�~�߂�
        if (player == null) return;

        //�v���C���[�U�����̓����蔻�菈��
        //Bullet�̊��N���X���擾
        if (collision.TryGetComponent<BulletBase>(out BulletBase bullet))
        {
            //�K�E�Z�|�C���g���߂�
            player.AddPoint(bullet.GetAddPoint);

            //Bullet�̃X�P�[���ύX
            bullet.SetScale();
            return;
        }
        //Enemy�̊��N���X���擾
        else if (collision.TryGetComponent<EnemyBase>(out EnemyBase enemy))
        {
            //�K�E�Z�|�C���g���߂�
            player.AddPoint(enemy.GetAddPoint);

            //Enemy�̃_���[�W����
            enemy.TakeDamage(takeDamageValue);
            return;
        }
    }
}
