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
    [SerializeField] GameObject Bullet;
    [SerializeField] Vector2 BulletScale = new Vector2 (1, 1);
    int SuperPowerPoint = 0;

    
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

    private void Move()
    {
        if (Input.GetKey(KeyCode.A)&&X_LeftLimit < transform.position.x) 
        {
            transform.position = new Vector2(transform.position.x-Speed*Time.deltaTime, transform.position.y);
        }
        if (Input.GetKey(KeyCode.D) && X_RightLimit > transform.position.x)
        {
            transform.position = new Vector2(transform.position.x + Speed * Time.deltaTime, transform.position.y);
        }
        if (Input.GetKey(KeyCode.W) && Y_UpLimit > transform.position.y)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + Speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S) && Y_DownLimit < transform.position.y)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - Speed * Time.deltaTime);
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
                GameObject CL_Bullet = Instantiate(Bullet, transform.position, Quaternion.identity);
                CL_Bullet.transform.position = new Vector2(transform.position.x, transform.position.y + BulletScale.y / 2);
                CL_Bullet.transform.localScale = BulletScale;

                Destroy(CL_Bullet, 0.1f);
            }
        }
    }
    void Active() 
    {
        Move();
        Attack();
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
    
    }
}
