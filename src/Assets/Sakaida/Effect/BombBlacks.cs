using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBlacks : MonoBehaviour
{
    [SerializeField] GameObject Black;
    float X;
    float Y;

    float Timer = 0;
    float ActiveTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        ActiveTimer += Time.deltaTime;
        if (ActiveTimer > 1.5) 
        {
            Destroy(gameObject);
        }
        if (Timer > 0.03)
        {
            for (int i = 0; i < 5; i++)
            {
                
                X = Random.Range(-8.5f, 8.5f);
                Y = Random.Range(-5, 5);
                GameObject CL_Black = Instantiate(Black);
                CL_Black.transform.position = new Vector2(X, Y);

                Destroy(CL_Black, 0.8f);
            }
            Timer = 0;
        }
        else 
        {
            Timer += Time.deltaTime;
        }
    }
}
