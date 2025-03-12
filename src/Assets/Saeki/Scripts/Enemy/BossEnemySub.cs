using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class BossEnemySub : EnemyBase
{
    [SerializeField] 
    ShotPatarnBase patarn;
    [SerializeField] 
    Transform shottingTransform;
    [SerializeField] 
    SpriteRenderer bossArmSpriteRenderer;
    protected override void EnemyUpDate()
    {
        //発射条件チェック
        if (patarn.PatarnCeangeLimit(timeCount))
            BulletShot();

        //色変更
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
        //残HPから色変更(減るほど赤)
        float value = HP / maxHP;
        bossArmSpriteRenderer.color = new Color(1, value, value, 1);
    }
    protected override void EnemyDead()
    {
        GameManager.AddScore(score);
        this.gameObject.SetActive(false);
    }

    /// <summary>
    /// 死亡判定
    /// </summary>
    /// <returns>死んだらtrue</returns>
    public bool IsDestroyed() => HP <= 0;
}
