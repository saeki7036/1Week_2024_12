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
        //���ˏ����`�F�b�N
        if (patarn.PatarnCeangeLimit(timeCount))
            BulletShot();

        //�F�ύX
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
        //�cHP����F�ύX(����قǐ�)
        float value = HP / maxHP;
        bossArmSpriteRenderer.color = new Color(1, value, value, 1);
    }
    protected override void EnemyDead()
    {
        GameManager.AddScore(score);
        this.gameObject.SetActive(false);
    }

    /// <summary>
    /// ���S����
    /// </summary>
    /// <returns>���񂾂�true</returns>
    public bool IsDestroyed() => HP <= 0;
}
