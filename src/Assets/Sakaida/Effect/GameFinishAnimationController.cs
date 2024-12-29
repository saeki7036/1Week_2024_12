using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class GameFinishAnimationController : MonoBehaviour
{

    [SerializeField] GameObject WhiteEffect;
    [SerializeField] GameObject BombWhite;
    [SerializeField] float ScaleUpSpeed = 50f;
    [SerializeField] GameObject BigScoreUI;
    [SerializeField] Player player;
    [SerializeField] GameObject Gate;
    [SerializeField] GameObject SGate;
    float X;
    float Y;

    public List<GameObject> UIs = new List<GameObject>();

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
        if (ActiveTimer < 4)
        {
            if (ActiveTimer > 3)
            {
                //Destroy(gameObject);

                if (BombWhite.transform.localScale.x < 30)
                {
                    Gate.SetActive(true);
                    //SGate.SetActive(true);
                    BombWhite.transform.localScale = new Vector2(BombWhite.transform.localScale.x + ScaleUpSpeed * Time.deltaTime, BombWhite.transform.localScale.y + ScaleUpSpeed * Time.deltaTime);
                    BombWhite.transform.position = new Vector2(-4.25f, 3.5f);
                    BombWhite.SetActive(true);


                    foreach (GameObject obj in UIs)
                    {
                        obj.SetActive(false);
                    }
                }

            }
            if (Timer > 0.08)
            {
                for (int i = 0; i < 2; i++)
                {
                    X = Random.Range(-8.5f, 0);
                    Y = Random.Range(2, 5);
                    GameObject CL_Black = Instantiate(WhiteEffect);
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
        else 
        {
            BigScoreUI.SetActive(true);
            player.AllLimitChange();
        }
    }
}

