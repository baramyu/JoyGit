using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
    waiting,
    inGame
}
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameState gameState;
    public AudioSource bgmAudioSource;
    public AudioSource soundAudioSource;
    public float swipeSensitivity;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void GameStart()
    {

        gameState = GameState.inGame;
    }
    public void GameOver()
    {
        gameState = GameState.waiting;
    }






    public Slider bgmSlider;
    public Text bgmValueText;
    public Slider soundSlider;
    public Text soundValueText;
    public Slider sensSlider;
    public Text sensValueText;

    public void BgmVolumeChange()
    {
        bgmAudioSource.volume = bgmSlider.value;
        bgmValueText.text = (bgmSlider.value * 100).ToString("N0");
    }


    public void SoundVolumeChange()
    {
        soundAudioSource.volume = soundSlider.value;
        soundValueText.text =  (soundSlider.value * 100).ToString("N0");
        soundAudioSource.Play();
    }
    public void SensChange()
    {
        swipeSensitivity = sensSlider.value;
        sensValueText.text = (sensSlider.value * 100).ToString("N0");
    }
}
