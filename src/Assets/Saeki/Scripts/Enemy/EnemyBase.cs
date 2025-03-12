using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField]
    protected int score = 50;
    [SerializeField]
    protected float HP = 1;
    [SerializeField]
    protected float Speed = 0.1f;

    [SerializeField]
    GameObject DieEffect;
    [SerializeField]
    AudioClip DieClip;

    AudioManager Audio => AudioManager.instance;

    protected int timeCount;
    protected float maxHP;
    private void Start()
    {
        //パラメータ設定
        maxHP = HP;
        timeCount = -200;//ある程度初撃タイミングを遅延
    }

    public void TakeDamage(float damage)
    {
        HP -= damage;
        if(HP <= 0) 
            EnemyDead();
    }

    protected virtual void EnemyDead()
    {
        //音声再生
        if (DieClip != null)
        {
            Audio.isPlaySE(DieClip);
        }
        else 
        {
            Debug.Log("死亡時効果音が入っていません");
        }
        //エフェクト生成
        Instantiate(DieEffect,this.transform.position,Quaternion.identity);
        //スコア加算
        GameManager.AddScore(score);
        Destroy(this.gameObject);
    }
   
    // Update is called once per frame
    void FixedUpdate()
    {
        //一定範囲内(画面内)の時に加算
        if(transform.position.y > -5.5f)
        timeCount++;
        EnemyUpDate();
    }

    protected virtual void EnemyUpDate()
    {
        return;//基底クラス
    }
}
