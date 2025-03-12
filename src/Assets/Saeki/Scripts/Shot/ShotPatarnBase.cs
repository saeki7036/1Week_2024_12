using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ShotPatarnBase : MonoBehaviour
{
    [SerializeField]
    int shotInterval = 50;

    public bool PatarnCeangeLimit(float time) => time >= shotInterval;
    

    public virtual void PatarnPlay(Transform enemyTransform)
    {
        return;//Šî’êƒNƒ‰ƒX
    }

}
