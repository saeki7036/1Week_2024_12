using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    [SerializeField]
    protected Rigidbody2D rb2D;

    [SerializeField]
    protected float speed = 0.01f;
    [SerializeField]
    int score = 10;
    
    [SerializeField]
    GameObject BulletDieEffect;
    [SerializeField]
    AudioClip DieClip;

    enum Scale
    {
        large,
        normal,
        small,
        zero,
    }

    [SerializeField]
    Scale scale = Scale.normal;

    AudioManager Audio => AudioManager.instance;

    protected Vector3 vector;

    public void SetVector(Vector3 vector3) => vector = vector3;

    void Start()
    {
        //�X�P�[�g��ݒ�
        this.transform.localScale = ScaleSetting();
        BulletSetUp();
    }

    protected virtual void BulletSetUp()
    {
        return;//���N���X
    }

    Vector3 ScaleSetting()
    {
        //�X�P�[���̃p�����[�^��Ԃ�
        switch (scale)
        {
            case Scale.large:
                return Vector3.one * 1.5f;
            case Scale.normal:
                return Vector3.one * 0.75f;
            case Scale.small:
                return Vector3.one * 0.3f;
        }
        return Vector3.zero;
    }

    int ScaleScore()
    {
        //enum�p�����[�^����X�R�A��Ԃ�
        switch (scale)
        {
            case Scale.large:
                return 4;
            case Scale.normal:
                return 3;
            case Scale.small:
                return 2;
        }
        return 1;
    }

    void ScaleDown()
    {
        switch (scale)
        {
            case Scale.large:
                scale = Scale.normal;
                break;
            case Scale.normal:
                scale = Scale.small;
                break;
            case Scale.small:
                scale = Scale.zero;
                break;
        }
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

        ScaleDown();
        Vector3 nextScale = ScaleSetting();
       
        //�X�P�[����0�Ȃ�j�󏈗�
        if (nextScale == Vector3.zero)
            DestroyBullet();

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

        Destroy(this.gameObject);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent<PlayerDamageArea>(out PlayerDamageArea player))
        {
            //�v���C���[�̔�e����
            player.TakeDamage(1);
            Debug.Log("Damage");
            Destroy (this.gameObject);
        }
        Debug.Log("Hit");
    }
}
