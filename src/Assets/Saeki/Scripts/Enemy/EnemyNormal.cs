using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNormal : EnemyBase
{
    [SerializeField] ShotPatarnBase patarn;

    protected override void EnemyUpDate()
    {
        transform.Translate(new Vector3(0f, -Speed, 0f));
        if(patarn.PatarnCeangeLimit(timeCount))
            BulletShot();
    }

    void BulletShot()
    {
        timeCount = 0;
        patarn.PatarnPlay(this.transform);
    }
}
