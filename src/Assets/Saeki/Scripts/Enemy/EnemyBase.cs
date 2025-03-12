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
        //�p�����[�^�ݒ�
        maxHP = HP;
        timeCount = -200;//������x�����^�C�~���O��x��
    }

    public void TakeDamage(float damage)
    {
        HP -= damage;
        if(HP <= 0) 
            EnemyDead();
    }

    protected virtual void EnemyDead()
    {
        //�����Đ�
        if (DieClip != null)
        {
            Audio.isPlaySE(DieClip);
        }
        else 
        {
            Debug.Log("���S�����ʉ��������Ă��܂���");
        }
        //�G�t�F�N�g����
        Instantiate(DieEffect,this.transform.position,Quaternion.identity);
        //�X�R�A���Z
        GameManager.AddScore(score);
        Destroy(this.gameObject);
    }
   
    // Update is called once per frame
    void FixedUpdate()
    {
        //���͈͓�(��ʓ�)�̎��ɉ��Z
        if(transform.position.y > -5.5f)
        timeCount++;
        EnemyUpDate();
    }

    protected virtual void EnemyUpDate()
    {
        return;//���N���X
    }
}
