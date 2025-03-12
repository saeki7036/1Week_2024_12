using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    TimeManager timeManager;
    [SerializeField]
    TimeTible[] timeTibles;

    int maxIndex;
    int currentIndex;
    int nextSpownTime;
    void Start()
    {
        //設定タイムテーブル個数
        maxIndex = timeTibles.Length;

        //初期値に設定
        currentIndex = 0;
        if (timeTibles.Length != 0)
            nextSpownTime = timeTibles[0].GetSpowntime;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //湧き判定
        if(SpownCheck(timeManager.Gettime) == true)
        {
            //生成処理
            EnemySpown();
            //次タイムテーブルを設定
            SetNextTimeTible();
        }
               
    }

    bool SpownCheck(int time)
    {
        //最後のタイムテーブルなら
        if (currentIndex >= maxIndex) return false;
        //時間がスポーン時間より小さいなら
        if (time <= nextSpownTime) return false;

        return true;
    }

    void EnemySpown()
    {
        //生成回数
        int length = timeTibles[currentIndex].GetInfomationLength;

        for(int i = 0; i < length; i++)
        {
            //構造体情報取得
            var tible = timeTibles[currentIndex].GetSpownInfomation(i);
            //敵生成
            Instantiate(tible.Enemy, tible.Point.position, Quaternion.identity);
        } 
    }

    void SetNextTimeTible()
    {
        //次のタイムテーブルに
        currentIndex++;
        //最後のタイムテーブルならreturn
        if (currentIndex >= maxIndex) return;
        //スポーン時間を取得
        nextSpownTime = timeTibles[currentIndex].GetSpowntime;
    }
}
