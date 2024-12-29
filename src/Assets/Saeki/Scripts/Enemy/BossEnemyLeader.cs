using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyLeader : EnemyBase
{

    [SerializeField] ShotPatarnBase[] patarns;
    [SerializeField] int patarnChengeInterval = 600;
    [SerializeField] SpriteRenderer bossSpriteRenderer;
    int patarnChengeCount = 0;

    bool superMode = false;

    public void SetSuperMode() => superMode = true;
    ShotPatarnBase currentPatarn;
    ShotPatarnBase PatarnChenge() => patarns[Random.Range(0, patarns.Length)];

    protected override void EnemyUpDate()
    {
        patarnChengeCount++;
        if (currentPatarn == null)
            currentPatarn = PatarnChenge();

        if (patarnChengeCount >= patarnChengeInterval)
        {
            patarnChengeCount = 0;
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

        if (superMode)  
            foreach (ShotPatarnBase Patarn in patarns)
                Patarn.PatarnPlay(this.transform);     
        else
            currentPatarn.PatarnPlay(this.transform);
    }
    void DamageColor()
    {
        float value = HP / maxHP;
        bossSpriteRenderer.color = new Color(1, value, value, 1);
    }
    protected override void EnemyDead()
    {
        GameManager.AddScore(score);
        this.gameObject.SetActive(false);
    }
}
