using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField]
    GameManager gameManager;//マネージャークラス

    [SerializeField] 
    TextMeshProUGUI timeText;//表示テキスト

    int time;//時間変数

    public int Gettime => time;//時間のゲッター

    // Start is called before the first frame update
    void Start()
    {
        time = 0;//初期化
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //ボスかプレイヤーが死ぬまで
        if(gameManager.IsMainGameState)
            time++;//加算

        //時間をTextに出力
        timeText.text = time.ToString();
    }
}
