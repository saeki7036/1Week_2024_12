using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] Animator Panimator;
    [SerializeField] GameObject PFinish;
    float PushTimer = 0;
    bool GameFinish = false;
    bool GameOver = true;
    // Start is called before the first frame update
    void Start()
    {
        GameFinish = false;
        GameOver = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.IsGameOver)
        {
            if (GameOver)
            {
                GameOver = false;
                GameOverScreen();
            }
        }
        else if (GameFinish)
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

    async void GameOverScreen()
    {
        var token = this.GetCancellationTokenOnDestroy();
        await UniTask.Delay(TimeSpan.FromSeconds(1f), cancellationToken: token);
        PFinish.SetActive(true);
        SetScreenScale();
        await UniTask.Delay(TimeSpan.FromSeconds(0.8f), cancellationToken: token);
        SceneManager.LoadScene("Sakaida_StartScene");
    }

    void SetScreenScale()
    {
        PFinish.transform.DOScale(Vector3.one * 65f, 1f).SetLink(PFinish);
    }
}
