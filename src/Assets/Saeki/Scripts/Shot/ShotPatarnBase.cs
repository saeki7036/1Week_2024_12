using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotPatarnBase : MonoBehaviour
{
    [SerializeField]
    int Interval = 50;

    public bool PatarnCeangeLimit(float time) => time >= Interval;
    
    

    public virtual void PatarnPlay(Transform enemyTransform)
    {

    }
    public virtual void PatarnEnter()
    {

    }
    public virtual void PatarnExit()
    {

    }
}
