using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageArea : MonoBehaviour
{

    [SerializeField] Player player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(int value) 
    { 
    player.TakeDamage(value);
    }
}
