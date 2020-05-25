using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    AudioSource bgmSource;
    [SerializeField]
    AudioSource fxSource;

    public AudioClip buttonHover;
    public AudioClip buttonClick;


    void Start()
    {
    }


    public void PlayerButtonHover()
    {
        fxSource.clip = buttonHover;
        fxSource.Play();
    }
    public void PlayerButtonClick()
    {
        fxSource.clip = buttonClick;
        fxSource.Play();
    }
}
