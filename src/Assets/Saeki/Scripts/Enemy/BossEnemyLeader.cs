using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyLeader : EnemyBase
{

    [SerializeField] 
    ShotPatarnBase[] patarns;
    [SerializeField] 
    int patarnChengeInterval = 600;
    [SerializeField] 
    SpriteRenderer bossSpriteRenderer;

    int patarnChengeCount = 0;

    bool superMode = false;

    public void SetSuperMode() => superMode = true;
    ShotPatarnBase currentPatarn;
    ShotPatarnBase PatarnChenge() => patarns[Random.Range(0, patarns.Length)];

    protected override void EnemyUpDate()
    {
        patarnChengeCount++;

        //パターン未設定なら設定する
        if (currentPatarn == null)
            currentPatarn = PatarnChenge();

        //パターン変更チェック
        if (patarnChengeCount >= patarnChengeInterval)
        {
            patarnChengeCount = 0;
            currentPatarn = PatarnChenge();
        }

        //発射条件チェック
        if (currentPatarn != null && currentPatarn.PatarnCeangeLimit(timeCount))     
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

        //全パターン同時に使う
        if (superMode)  
            foreach (ShotPatarnBase Patarn in patarns)
                Patarn.PatarnPlay(this.transform);   
        //現在のパターンのみ使う
        else
            currentPatarn.PatarnPlay(this.transform);
    }
    void DamageColor()
    {
        //残HPから色変更(減るほど赤)
        float value = HP / maxHP;
        bossSpriteRenderer.color = new Color(1, value, value, 1);
    }
    protected override void EnemyDead()
    {
        GameManager.AddScore(score);
        this.gameObject.SetActive(false);
    }
}
