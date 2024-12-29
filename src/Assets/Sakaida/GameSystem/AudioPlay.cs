using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlay : MonoBehaviour
{
    [SerializeField] AudioSource Asource;

    // Start is called before the first frame update
    void Start()
    {
        //Asource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Asource.isPlaying)
        {
            Destroy(gameObject);
        }
    }
    public void isPlaySE(AudioClip Clip , float vol)
    {
        Asource.volume = vol;
        Asource.clip = Clip;
        Asource.Play();
    }
}
