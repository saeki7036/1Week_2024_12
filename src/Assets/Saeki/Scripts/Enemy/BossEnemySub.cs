using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemySub : EnemyBase
{
    [SerializeField] 
    ShotPatarnBase patarn;//�s���p�^�[��

    [SerializeField] 
    Transform shottingTransform;//���ˈʒu�̍��W

    [SerializeField] 
    SpriteRenderer bossArmSpriteRenderer;//�F�ύX�Ɏg�������_���[

    float maxHP;//�ő�̗�

    //�p�����[�^�ݒ�
    protected override void EnemySetUp()
    {
        maxHP = HP;//�ő�̗͎擾
    }

    //�A�b�v�f�[�g
    protected override void EnemyUpDate()
    {
        //���ˏ����`�F�b�N
        if (patarn.PatarnCeangeLimit(shotTimeCount))
            BulletShot();//���ˏ���

        //�I�u�W�F�N�g���\������Ă���Ȃ�F�ύX
        if (this.gameObject.activeSelf)
        {
            //�F�ύX
            DamageColor();
        }
    }

    //���ˏ���
    void BulletShot()
    {
        //�J�E���g������
        shotTimeCount = 0;

        //���˃p�^�[���N��
        patarn.PatarnPlay(shottingTransform);
    }

    //�cHP����F�ύX(����قǐ�)
    void DamageColor()
    {
        //�����v�Z
        float value = HP / maxHP;

        //G��B��HP������ɂ�Đ��l��������Ԃ��Ȃ�
        bossArmSpriteRenderer.color = new Color(1, value, value, 1);
    }

    //���S����
    protected override void EnemyDead()
    {
        //�X�R�A���Z
        GameManager.AddScore(score);

        //�I�u�W�F�N�g���̂��\����
        this.gameObject.SetActive(false);
    }

    /// <summary>
    /// �j�󔻒�
    /// </summary>
    /// <returns>�j�󂵂Ă�����true</returns>
    public bool IsDestroyed() => HP <= 0;
}
