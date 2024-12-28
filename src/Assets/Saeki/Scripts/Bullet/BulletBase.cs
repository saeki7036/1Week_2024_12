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

    enum Scale
    {
        large,
        normal,
        small,
        zero,
    }

    [SerializeField]
    Scale scale = Scale.normal;


    protected Vector3 vector;

    public void SetVector(Vector3 vector3) => vector = vector3;

    void Start()
    {
        this.transform.localScale = ScaleSetting();
        BulletSetUp();
    }

    protected virtual void BulletSetUp()
    {

    }

    Vector3 ScaleSetting()
    {
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
        GameManager.AddScore(score * ScaleScore());
        DestroyBullet();

        
    }

    public void SetScale()
    {
        GameManager.AddScore(score * ScaleScore());
        ScaleDown();
        Vector3 nextScale = ScaleSetting();
       
        if (nextScale == Vector3.zero)
            DestroyBullet();

        this.transform.localScale = nextScale;
    }

    void DestroyBullet()
    {
        GameObject CL_BulletDieEffect = Instantiate(BulletDieEffect,transform.position,Quaternion.identity);
        Destroy(this.gameObject);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent<PlayerDamageArea>(out PlayerDamageArea player))
        {
            player.TakeDamage(1);
            Debug.Log("Damage");
            Destroy (this.gameObject);
        }
        Debug.Log("Hit");
    }
}
