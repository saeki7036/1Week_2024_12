using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField]
    GameManager gameManager;
    int time;

    public int Gettime => time;
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(gameManager.IsMainGameState)
            time++;

        Debug.Log(time);
    }
}