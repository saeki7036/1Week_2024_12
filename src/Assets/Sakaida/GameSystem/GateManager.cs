using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GateManager : MonoBehaviour
{

    [SerializeField] GameObject GateEffect;

    bool GameStart = false;

    public float forceMagnitude = 10f;

    [SerializeField] bool GameStartGate = true; 

    float Timer = 0;
    float EffectTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (EffectTimer > 0.05)
        {
            EffectTimer = 0;

            GameObject CL_Effect = Instantiate(GateEffect,transform.position,Quaternion.identity);
            Destroy(CL_Effect, 0.2f);
            float angle = Random.Range(0f, 360f);



            float angleInRadians = angle * Mathf.Deg2Rad;
            Vector2 forceDirection = new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians));
            Rigidbody2D rb = CL_Effect.GetComponent<Rigidbody2D>();

            rb.AddForce(forceDirection * forceMagnitude, ForceMode2D.Impulse);
        }
        else 
        {
            EffectTimer += Time.deltaTime;
        }

        if (GameStart) 
        {
            Timer += Time.deltaTime;
            transform.localScale = new Vector3(transform.localScale.x+2, transform.localScale.y+2);
            if (Timer > 0.5) 
            {
                if (GameStartGate)
                {
                    SceneManager.LoadScene("Sakaida_Scene");
                }
                else
                {
                    SceneManager.LoadScene("Sakaida_StartScene");
                }
            }
        }
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            //SceneManager.LoadScene("Sakaida_Scene");
            GameStart = true;
        }
    }
}
