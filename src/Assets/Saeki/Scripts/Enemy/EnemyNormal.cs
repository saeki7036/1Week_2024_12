using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNormal : EnemyBase
{
    [SerializeField] ShotPatarnBase patarn;

    //�P�̃e�X�gOK
    protected override void EnemyUpDate()
    {
        //�ړ�
        transform.Translate(new Vector3(0f, -Speed, 0f));
        //���˃`�F�b�N
        if(patarn.PatarnCeangeLimit(timeCount))
            BulletShot();
    }

    void BulletShot()
    {
        timeCount = 0;
        patarn.PatarnPlay(this.transform);
    }
}
