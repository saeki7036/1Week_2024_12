using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] Animator playerAnim;
    [SerializeField] Slider SuperPowerSlider;
    int SuperPowerPoint = 0;
    [SerializeField] Animator[] Casets;
    [SerializeField] GameObject dieEffect;
    [SerializeField] TextMeshProUGUI PowerPointText;
    public void AddPoint(int point) => SuperPowerPoint += point;
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
        if(Casets.Length != 0)
        {
            Casets[0]?.SetInteger("Anim", 0);
            Casets[1]?.SetInteger("Anim", 0);
        }     
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

        if (PowerPointText != null)
        {
            PowerPointText.text = SuperPowerPoint.ToString();
            PowerPointText.color = SuperPowerPoint > 100 ? Color.magenta : Color.white;
        }
       
        SuperPowerChenge();
    }

    void SuperPowerChenge()
    {
        if (SuperPowerSlider == null)
            return;
        if (SuperPowerPoint >= 100)
        {
            
            if (HP < 3)
            {
                Casets[HP]?.SetInteger("Anim", 0);
                HP++;
                SuperPowerPoint -= 100;
            }
                
        }
        SuperPowerSlider.value = SuperPowerPoint;
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
        if (value == 0)
        {
            if (SuperPowerPoint >= 100)
                SuperPowerPoint -= 100;
            else
            {
                HP -= 1;
                if (HP > -1)
                    Casets[HP]?.SetInteger("Anim", 1);
            }        
        }
        else
        {
            if (SuperPowerPoint >= 100)
                SuperPowerPoint = 100;

            HP -= value;
            if (HP > -1)
                Casets[HP]?.SetInteger("Anim", 1);
        }
       
        if (HP < 0) 
        {
            Die();
        }
    }
    private async void Die()
    {
        GameObject effect = Instantiate(dieEffect, transform.position, Quaternion.identity);
        var token = this.GetCancellationTokenOnDestroy();
        await UniTask.Delay(TimeSpan.FromSeconds(0.1f), cancellationToken: token);
        this.gameObject.SetActive(false);
    }
    void Bomb() 
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && HP >= 2) 
        {
            TakeDamage(0);
            int number = UnityEngine.Random.Range(0, SuperAttacks.Length);
            SuperAttacks[number].PlaySuperAttack();
        }
    }
}
