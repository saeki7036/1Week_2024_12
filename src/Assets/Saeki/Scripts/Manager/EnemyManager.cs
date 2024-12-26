using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    TimeManager timeManager;
    [SerializeField]
    TimeTible[] timeTibles;

    int MaxIndex;
    int CurrentIndex;
    int NextSpownTime;
    void Start()
    {
        MaxIndex = timeTibles.Length;
        CurrentIndex = 0;
        if (timeTibles.Length != 0)
            NextSpownTime = timeTibles[0].GetSpowntime;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(SpownCheck(timeManager.Gettime) == true)
        {
            EnemySpown();
            SetNextTimeTible();
        }
               
    }

    bool SpownCheck(int time)
    {
        if (CurrentIndex >= MaxIndex) return false;
        if (time > NextSpownTime) return false;

        return true;
    }

    void EnemySpown()
    {
        int length = timeTibles[CurrentIndex].GetInfomationLength;

        for(int i = 0; i < length; i++)
        {
            var tible = timeTibles[CurrentIndex].GetSpownInfomation(i);

            Instantiate(tible.Enemy, tible.Point.position, Quaternion.identity);
        } 
    }

    void SetNextTimeTible()
    {
        CurrentIndex++;
        if (CurrentIndex >= MaxIndex) return;
        NextSpownTime = timeTibles[CurrentIndex].GetSpowntime;
    }
}
