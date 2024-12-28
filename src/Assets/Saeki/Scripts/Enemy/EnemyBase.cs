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

    protected int timeCount;
    protected float maxHP;
    private void Start()
    {
        maxHP = HP;
        timeCount = -200;
    }

    public void TakeDamage(float damage)
    {
        HP -= damage;
        if(HP <= 0) 
            EnemyDead();
    }

    protected virtual void EnemyDead()
    {
        GameManager.AddScore(score);
        Destroy(this.gameObject);
    }
   
    // Update is called once per frame
    void FixedUpdate()
    {
        if(transform.position.y > -5.5f)
        timeCount++;
        EnemyUpDate();
    }

    protected virtual void EnemyUpDate()
    {

    }
}
