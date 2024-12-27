using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieEffectController : MonoBehaviour
{
    [SerializeField] GameObject Effect;
    float Timer = 0;
    int count = 0;
    float random = 0;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 9; i++)
        {
            Timer = 0;
            GameObject CL_Effect = Instantiate(Effect);
            Rigidbody2D rb = CL_Effect.GetComponent<Rigidbody2D>();
            random = Random.Range(0, 360);

            float Ragi = random * Mathf.Deg2Rad;
            Vector2 forceDirection = new Vector2(Mathf.Cos(Ragi), Mathf.Sin(Ragi));

            rb.AddForce(forceDirection * 3.5f, ForceMode2D.Impulse);

            Destroy(CL_Effect, 2);
        }
        Destroy(gameObject);
    }

    // Update is called once per frame
    
}
