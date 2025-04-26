using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNormal : EnemyBase
{
    [SerializeField] ShotPatarnBase patarn;//���˃p�^�[���̃N���X

    //�P�̃e�X�gOK
    protected override void EnemyUpDate()
    {
        //�ړ�
        transform.Translate(new Vector3(0f, -Speed, 0f));

        //���˃`�F�b�N
        if(patarn.PatarnCeangeLimit(shotTimeCount))
            BulletShot();
    }

    //���ˏ���
    void BulletShot()
    {
        //�J�E���g������
        shotTimeCount = 0;

        //���˃p�^�[���N��
        patarn.PatarnPlay(this.transform);
    }
}
