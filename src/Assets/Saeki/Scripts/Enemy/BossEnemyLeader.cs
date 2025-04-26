using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyLeader : EnemyBase
{
    [SerializeField] 
    ShotPatarnBase[] patarns;//�s���p�^�[���N���X�Q

    [SerializeField] 
    int patarnChengeInterval = 600;//�s���p�^�[���؂�ւ��C���^�[�o��

    [SerializeField] 
    SpriteRenderer bossSpriteRenderer;//�F�ύX�Ɏg�������_���[

    int patarnChengeCount = 0;//�s���p�^�[���؂�ւ��J�E���^�[

    bool superMode = false;//�s���p�^�[���؂�ւ��t���O

    //�s���p�^�[���؂�ւ��t���O�X�V�Z�b�^�[
    public void SetSuperMode() => superMode = true;

    //���݂̔��˃p�^�[��
    ShotPatarnBase currentPatarn;

    //���˃p�^�[���������_���ɑI��
    ShotPatarnBase PatarnChenge() => patarns[Random.Range(0, patarns.Length)];

     float maxHP;//�ő�̗�

    //�p�����[�^�ݒ�
    protected override void EnemySetUp()
    {
        maxHP = HP;//�ő�̗͎擾
    }

    //�A�b�v�f�[�g
    protected override void EnemyUpDate()
    {
        //�p�^�[���̐؂�ւ��J�E���g���Z
        patarnChengeCount++;

        //�p�^�[�����ݒ�Ȃ�ݒ肷��
        if (currentPatarn == null)
            currentPatarn = PatarnChenge();

        //�p�^�[���ύX�`�F�b�N
        //�p�^�[���̐؂�ւ��J�E���g��Interval�ȏ�Ő؂�ւ�
        if (patarnChengeCount >= patarnChengeInterval)
        {
            //�p�^�[���J�E���g������
            patarnChengeCount = 0;

            //�p�^�[���ύX
            currentPatarn = PatarnChenge();
        }

        //���ˏ����`�F�b�N
        //���˃p�^�[���̐ݒ肪���Ȃ��������ׂ�
        if (currentPatarn != null && currentPatarn.PatarnCeangeLimit(shotTimeCount))     
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

        //�S�p�^�[�������Ɏg��
        if (superMode)  
            foreach (ShotPatarnBase Patarn in patarns)
                Patarn.PatarnPlay(this.transform);  
        
        //���݂̃p�^�[���̂ݎg��
        else
            currentPatarn.PatarnPlay(this.transform);
    }

    //�cHP����F�ύX(����قǐ�)
    void DamageColor()
    {
        //�����v�Z
        float value = HP / maxHP;

        //G��B��HP������ɂ�Đ��l��������Ԃ��Ȃ�
        bossSpriteRenderer.color = new Color(1, value, value, 1);
    }

    //���S����
    protected override void EnemyDead()
    {
        //�X�R�A���Z
        GameManager.AddScore(score);

        //�I�u�W�F�N�g���̂��\����
        this.gameObject.SetActive(false);
    }
}
