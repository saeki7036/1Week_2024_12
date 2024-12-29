using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int HP = 3;

    public float Speed = 1f;
    public float AttackIntreval = 0.2f;
    float AttackTimer = 0;
    public float DamageIntreval = 1;
    [Space]
    [SerializeField] float X_RightLimit;
    [SerializeField] float X_LeftLimit;
    [SerializeField] float Y_UpLimit;
    [SerializeField] float Y_DownLimit;
    [Space]
    [SerializeField] GameObject AttackScreen;
    [SerializeField] Vector2 BulletScale = new Vector2 (1, 1);
    [SerializeField] SuperAttackBase[] SuperAttacks;
    int SuperPowerPoint = 0;
    [SerializeField] Animator playerAnim;

    public bool IsDard => HP <= 0;
    public enum TYPE 
    { 
    Active,
    noActive
    }
    public TYPE type = TYPE.Active;

    [SerializeField] AudioClip Clip1;

    AudioManager audio => AudioManager.instance;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (type) 
        { 
        
            case TYPE.Active:
                Active();
                break;
            case TYPE.noActive:
                
                break;


        }

        

    }
    public void AllLimitChange() 
    {
        X_RightLimit = 8.5f;
        X_LeftLimit = -8.5f;
        Y_UpLimit =5;
        Y_DownLimit =-5;
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {

            if (Input.GetKey(KeyCode.A) && X_LeftLimit < transform.position.x)
            {
                playerAnim.SetInteger("Anim", 1);
                transform.position = new Vector2(transform.position.x - Speed * Time.deltaTime, transform.position.y);
            }
            if (Input.GetKey(KeyCode.D) && X_RightLimit > transform.position.x)
            {
                playerAnim.SetInteger("Anim", 2);
                transform.position = new Vector2(transform.position.x + Speed * Time.deltaTime, transform.position.y);
            }
        }
        else 
        {
            playerAnim.SetInteger("Anim", 0);
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            if (Input.GetKey(KeyCode.W) && Y_UpLimit > transform.position.y)
            {
                transform.position = new Vector2(transform.position.x, transform.position.y + Speed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.S) && Y_DownLimit < transform.position.y)
            {
                transform.position = new Vector2(transform.position.x, transform.position.y - Speed * Time.deltaTime);
            }
        }
    }
    void Attack() 
    {
        if (AttackIntreval > AttackTimer)
        {
            AttackTimer += Time.deltaTime;
        }
        else
        {

            if (Input.GetKey(KeyCode.Space))
            {
                audio.isPlaySE(Clip1);
                AttackTimer = 0;

                AttackScreen.transform.position = new Vector2(transform.position.x, transform.position.y + BulletScale.y / 2);
                AttackScreen.transform.localScale = BulletScale;

                SetActiveAttackScreen();
            }
        }
    }

    async void SetActiveAttackScreen()
    {
        var token = this.GetCancellationTokenOnDestroy();

        AttackScreen.SetActive(true);
        await UniTask.DelayFrame(15,cancellationToken: token);
        AttackScreen.SetActive(false);
    }

    void Active() 
    {
        Move();
        Attack();
        Bomb();
    }
    public  void TakeDamage(int value)
    {
        HP -= value;
        if (HP < 0) 
        {
            Die();
        }
    }
    private void Die()
    {

        Debug.Log("SinDAAAAAAAAAAAAAAAAAAAAAAA");

    }
    void Bomb() 
    {
        if (Input.GetKeyDown(KeyCode.LeftShift)) 
        {
            int number = UnityEngine.Random.Range(0, SuperAttacks.Length);
            SuperAttacks[number].PlaySuperAttack();
        }
    }
}
