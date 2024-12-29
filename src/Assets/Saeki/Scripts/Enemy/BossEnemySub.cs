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
        float value = HP / maxHP;
        bossArmSpriteRenderer.color = new Color(1, value, value, 1);
    }
    protected override void EnemyDead()
    {
        GameManager.AddScore(score);
        this.gameObject.SetActive(false);
    }

    public bool IsDestroyed() => HP <= 0;
}
