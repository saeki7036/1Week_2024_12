using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackUI : MonoBehaviour
{
    [SerializeField] GameObject BackUIOBJ;

    float Timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Timer > 0.05)
        {
            Timer = 0;
            GameObject CL_BackUI = Instantiate(BackUIOBJ, transform.position, Quaternion.identity);
            float Randomx = Random.Range(-9, 9);

            CL_BackUI.transform.position = new Vector2(transform.position.x + Randomx, transform.position.y + 1);
            Rigidbody2D rb = CL_BackUI.GetComponent<Rigidbody2D>();

            rb.AddForce(transform.up * 200);
            Destroy(CL_BackUI, 2f);
        }
        else 
        {
            Timer += Time.deltaTime;
        }
        
    }
}
