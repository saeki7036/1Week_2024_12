using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField]
    protected int score = 50;//�h����ł��g���X�R�A���l
    [SerializeField]
    protected float HP = 1;//�h����ł��g��
    [SerializeField]
    protected float Speed = 0.1f;//�h����ł��g��Enemy�̈ړ����x

    [SerializeField]
    GameObject DieEffect;//���S���̃G�t�F�N�g�̃I�u�W�F�N�g

    [SerializeField]
    AudioClip DieClip;//���S���̉���

    [SerializeField]
    int addSuperAttackPoint = 10;//�v���C���[�����U���������ɓ�����|�C���g

    [SerializeField]
    int firstShotDalayValue = -200;//�����^�C�~���O�̒x��

    public int GetAddPoint => addSuperAttackPoint;//������|�C���g�̃Q�b�^�[

    AudioManager Audio => AudioManager.instance;

    protected int shotTimeCount;//�h����ł��g��

    const float shotCountLimitY = -5.5f;//��ʓ��ł̈ʒu

    private void Start()
    {
        shotTimeCount = firstShotDalayValue;//������x�����^�C�~���O��x��

        //�p�����[�^�ݒ�
        EnemySetUp();
    }

    protected virtual void EnemySetUp()
    {
        return;//���N���X
    }

    //�_���[�W����
    public void TakeDamage(float damage)
    {
        //�_���[�W���Z
        HP -= damage;

        //HP�Ȃ��Ȃ����玀�S����
        if(HP <= 0) 
            EnemyDead();
    }

    //���S����
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

        //�G���폜
        Destroy(this.gameObject);
    }
   
    // Update is called once per frame
    void FixedUpdate()
    {
        //���͈͓�(��ʓ�)�̎��ɉ��Z
        if(transform.position.y > shotCountLimitY)
        shotTimeCount++;

        //�h����̏���
        EnemyUpDate();
    }

    protected virtual void EnemyUpDate()
    {
        return;//���N���X
    }
}
