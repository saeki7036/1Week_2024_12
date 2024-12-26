using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    [SerializeField] Animator Panimator;
    [SerializeField] GameObject PFinish;
    float PushTimer = 0;
    bool GameFinish = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (GameFinish)
        {
            PFinish.SetActive(true);
            PFinish.transform.localScale = new Vector2(PFinish.transform.localScale.x+65f*Time.deltaTime, PFinish.transform.localScale.y+65f * Time.deltaTime);
            PushTimer += Time.deltaTime;
            if (PushTimer > 0.8) 
            {
                SceneManager.LoadScene("Sakaida_StartScene");
            }
        }
        else 
        { 
        if (Input.GetKey(KeyCode.P)) 
        {
            PushTimer += Time.deltaTime;
            Panimator.SetInteger("Anim", 1);
            if (PushTimer > 0.8) 
            { 
            GameFinish = true;PushTimer = 0;
            }
        }
        if (Input.GetKeyUp(KeyCode.P)) 
        {
            PushTimer = 0;
            Panimator.SetInteger("Anim", 0);
        }
        }
    }
}
