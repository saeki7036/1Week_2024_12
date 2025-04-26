using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{

    [SerializeField]
    protected Rigidbody2D rb2D;//�h����Ŏg�� Rigidbody2D

    [SerializeField]
    protected float speed = 0.01f;//�h����Ŏg���e�̑��x

    [SerializeField]
    int score = 10;//�X�R�A�̊�{���l
    
    [SerializeField]
    GameObject BulletDieEffect;//bullet�̔j�󎞂ɍĐ�����G�t�F�N�g�̃I�u�W�F�N�g

    [SerializeField]
    AudioClip DieClip; //�����N���b�v

    enum Scale//�X�P�[���p�����[�^��enum
    {
        large,
        normal,
        small,
        zero,
    }

    [SerializeField]
    Scale scale = Scale.normal;//�e�̑傫���̃p�����[�^

    [SerializeField]
    int addSuperAttackPoint = 5;//�v���C���[�����U���������ɓ�����|�C���g

    public int GetAddPoint => addSuperAttackPoint;//�Q�b�^�[

    AudioManager Audio => AudioManager.instance;//�C���X�^���X�̃����_��

    //transform�ɑ������X�P�[����float�p�����[�^�̐ςɎg�p
    const float largeScaleValue = 1.5f,
                normalScaleValue = 0.75f,
                smallScaleValue = 0.3f;

    Vector3 ScaleSetting()
    {
        //transform�ɑ������X�P�[���̎��ۂ̐��l��Ԃ�
        switch (scale)
        {
            //Vector3.one��float�p�����[�^�̐ςŌv�Z
            case Scale.large:
                return Vector3.one * largeScaleValue;

            case Scale.normal:
                return Vector3.one * normalScaleValue;

            case Scale.small:
                return Vector3.one * smallScaleValue;
        }

        //scale��zero�y�є͈͊O��0��Ԃ�
        return Vector3.zero;
    }

    //�X�R�A�v�Z�Ɏg�p����{���p�����[�^
    const int largeScoreValue = 4,
              normalScoreValue = 3,
              smallScoreValue = 2,
              zeroScoreValue = 1;
         
    int ScaleScore()
    {
        //enum�p�����[�^����X�R�A�{����Ԃ�
        switch (scale)
        {
            case Scale.large:
                return largeScoreValue;

            case Scale.normal:
                return normalScoreValue;

            case Scale.small:
                return smallScoreValue;
        }

        //scale = zero�͊�{�I�ɗ�O
        Debug.LogWarning("scale �� zero�ɏ��������s���ꂽ");
        return zeroScoreValue;
    }

    void ScaleDown()
    {
        //scale����i�K�ύX����
        switch (scale)
        {
            //large -> normal
            case Scale.large:
                scale = Scale.normal;
                break;

            //normal -> small
            case Scale.normal:
                scale = Scale.small;
                break;

            //small -> zero
            case Scale.small:
                scale = Scale.zero;
                break;
        }
        //zero�͕ω����Ȃ�
    }

    void Start()
    {
        //�X�P�[����ݒ�
        this.transform.localScale = ScaleSetting();

        //�h����̃N���X�̌Ăяo��
        BulletSetUp();
    }

    protected virtual void BulletSetUp()
    {
        return;//���N���X
    }

    public void KillBullet()
    {
        //�X�R�A���Z
        GameManager.AddScore(score * ScaleScore());

        //�j�󏈗�
        DestroyBullet();
    }

    public void SetScale()
    {
        //�X�R�A���Z
        GameManager.AddScore(score * ScaleScore());

        //Enum�ϐ�
        ScaleDown();

        //�X�P�[���擾
        Vector3 nextScale = ScaleSetting();
       
        //�X�P�[����0�Ȃ�j�󏈗�
        if (nextScale == Vector3.zero)
            DestroyBullet();

        //�X�P�[�����Z�b�g
        this.transform.localScale = nextScale;
    }

    void DestroyBullet()
    {
        //�����Đ�
        if (DieClip != null)
        {
            Audio.isPlaySE(DieClip);
        }
        else 
        {
            Debug.Log("�����Z�b�g����ĂȂ���");
        }

        //�G�t�F�N�g����
        Instantiate(BulletDieEffect,transform.position,Quaternion.identity);

        //�I�u�W�F�N�g�폜
        Destroy(this.gameObject);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        //�v���C���[�̓����蔻�菈���N���X���擾
        if(other.TryGetComponent<PlayerDamageArea>(out PlayerDamageArea player))
        {
            //�v���C���[�̔�e����
            player.TakeDamage(1);

            //�_���[�W���O
            Debug.Log("Damage");

            //��x��e�����e�͍폜
            Destroy(this.gameObject);
        }

        //��e���O
        //Debug.Log("Hit");
    }
}
