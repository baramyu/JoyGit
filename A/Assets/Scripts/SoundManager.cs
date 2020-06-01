using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;


    [SerializeField]
    AudioSource bgmSource;
    [SerializeField]
    AudioSource fxSource;

    public AudioClip buttonHover;
    public AudioClip buttonClick;

    private void Awake()
    {
        instance = this;
    }

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

    public void FadeOutBgm(float fadeTime)
    {
        StartCoroutine(FadeOutBgmCor(fadeTime));
    }

    IEnumerator FadeOutBgmCor(float fadeTime)
    {
        float startVolume = bgmSource.volume;

        while (bgmSource.volume > 0)
        {
            bgmSource.volume -= startVolume * Time.deltaTime / fadeTime;

            yield return null;
        }
    }

    public void SetBgmVolume(float volume)
    {
        bgmSource.volume = volume;
    }
    public void SetFxVolume(float volume)
    {
        fxSource.volume = volume;
    }
}
