using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAttackArea : MonoBehaviour
{
    [SerializeField]
    float magnifyScaleX = 23;//�X�P�[����

    [SerializeField]
    float magnifyTime = 2;//�X�P�[���ύX����

    [SerializeField] 
    int loopCount = 2;//���[�v��

    [SerializeField]
    float delayTime = 0.5f;//�x������

    [SerializeField]
    float takeDamageValue = 200f;//�G�ɗ^����_���[�W��

    const LoopType loopType = LoopType.Yoyo;//���[�v�^�C�v

    Player player;//Prehub����Ăяo���̂�Serialize���Ȃ�

    void Start()
    {
        //player��null�Ȃ�T�����s��(Player��Tag��Scene�Ɉ�����Ȃ̂�O(1)�B)
        if (player == null)
        {
            GameObject playerObject = GameObject.FindWithTag("Player");

            //PlayerObject�ɂ���player�N���X��TryGet�ő��(��{��������)
            if (playerObject != null && playerObject.TryGetComponent<Player>(out var p))
                player = p;
        }
    }

    /// <summary>
    /// �U������̂���G���A�𓮂���
    /// </summary>
    public void MoveArea()
    {
        //���̃I�u�W�F�N�g���A�N�e�B�u��Ԃ�
        this.gameObject.SetActive(true);

        //DOTween���g����X�̃X�P�[����ό`
        this.transform
            .DOScaleX(magnifyScaleX, magnifyTime)//�w�肵���l�ɃX�P�[��
            .SetLoops(loopCount, loopType)//��������[�v������
            .SetDelay(delayTime).//�A�j���[�V�����̊J�n��x�点��
        OnComplete(() =>//������
        {
            //�������A�N�e�B�u��Ԃ�
            this.gameObject.SetActive(false);

            //�I�u�W�F�N�g�폜
            Destroy(this.gameObject);
        });
    }

    //�U�����菈��
    void OnTriggerEnter2D(Collider2D collision)
    {
        //player��null�Ȃ�~�߂�
        if (player == null) return;

        //�v���C���[�̕K�E�U�����̓����蔻�菈��
        //Bullet�̊��N���X���擾
        if (collision.TryGetComponent<BulletBase>(out BulletBase bullet))
        {
            //�K�E�Z���߂�
            player.AddPoint(bullet.GetAddPoint);

            //�e�̍폜���s��
            bullet.KillBullet();
            
            return;
        }
        //Enemy�̊��N���X���擾
        else if (collision.TryGetComponent<EnemyBase>(out EnemyBase enemy))
        {
            //�K�E�Z���߂�
            player.AddPoint(enemy.GetAddPoint);

            //�_���[�W����
            enemy.TakeDamage(takeDamageValue);

            return;
        }
    }
}
