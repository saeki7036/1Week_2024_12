using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNormal : EnemyBase
{
    [SerializeField] ShotPatarnBase patarn;

    //単体テストOK
    protected override void EnemyUpDate()
    {
        //移動
        transform.Translate(new Vector3(0f, -Speed, 0f));
        //発射チェック
        if(patarn.PatarnCeangeLimit(timeCount))
            BulletShot();
    }

    void BulletShot()
    {
        timeCount = 0;
        patarn.PatarnPlay(this.transform);
    }
}
