using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyLeader : EnemyBase
{

    [SerializeField] ShotPatarnBase[] patarns;
    [SerializeField] int patarnChengeInterval = 600;
    [SerializeField] SpriteRenderer bossSpriteRenderer;
    int patarnChengeCount = 0;

    ShotPatarnBase currentPatarn;
    ShotPatarnBase PatarnChenge() => patarns[Random.Range(0, patarns.Length)];

    protected override void EnemyUpDate()
    {


        patarnChengeCount++;
        if(patarnChengeCount >= patarnChengeInterval)
        {
            patarnChengeCount++;
            if (currentPatarn == null)
                currentPatarn = PatarnChenge();
        }

        if (currentPatarn != null && currentPatarn.PatarnCeangeLimit(timeCount))
            BulletShot();

        if (this.gameObject.activeSelf)
        {
            DamageColor();
        }
    }
    void BulletShot()
    {
        timeCount = 0;
        currentPatarn.PatarnPlay(this.transform);
    }
    void DamageColor()
    {
        bossSpriteRenderer.color = new Color(1 - (maxHP / HP), 0, 0, 1);
    }
    protected override void EnemyDead()
    {
        GameManager.AddScore(score);
        this.gameObject.SetActive(false);
    }
}
