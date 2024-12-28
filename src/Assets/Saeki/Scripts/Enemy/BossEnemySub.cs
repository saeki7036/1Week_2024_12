using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class BossEnemySub : EnemyBase
{
    [SerializeField] ShotPatarnBase patarn;
    [SerializeField] Transform shottingTransform;
    [SerializeField] SpriteRenderer bossArmSpriteRenderer;
    protected override void EnemyUpDate()
    {
        if (patarn.PatarnCeangeLimit(timeCount))
            BulletShot();

        if (this.gameObject.activeSelf)
        {
            DamageColor();
        }
    }
    void BulletShot()
    {
        timeCount = 0;
        patarn.PatarnPlay(shottingTransform);
    }

    void DamageColor()
    {
        bossArmSpriteRenderer.color = new Color(1-(maxHP / HP),0,0,1);
    }
    protected override void EnemyDead()
    {
        GameManager.AddScore(score);
        this.gameObject.SetActive(false);
    }

    public bool IsDestroyed() => HP <= 0;
}
