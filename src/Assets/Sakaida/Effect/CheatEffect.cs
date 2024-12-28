using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatEffect : MonoBehaviour
{
    [SerializeField] GameObject TargetPoint;
    [SerializeField] float Speed = 5;
    

    // Start is called before the first frame update
    void Start()
    {
        TargetPoint = GameObject.Find("SliderPoint");
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, TargetPoint.transform.position, Speed * Time.deltaTime);
        
        Destroy(gameObject,2f);
        
    }
}
