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
    public float settlingSpeedLimit;
    public float settlingAngleLimit;
    public Color cubeColor;
    public Color coreColor;
    public Material cubeMaterial;
    public Material coreMaterial;
    public Image startButtonImage;
    public Image optionButtonImage;
    public Image optionPanelImage;
    public Image gridButtonImage;
    public MotherCube motherCube;
    public GameObject grid;
    public Text gridButtonText;
    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Coloring();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Coloring()
    {
        cubeColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        coreColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        cubeMaterial.color = cubeColor;
        coreMaterial.color = coreColor;
        startButtonImage.color = cubeColor;
        optionButtonImage.color = cubeColor;
        optionPanelImage.color = cubeColor;
        gridButtonImage.color = cubeColor;
    }
    public void GameStart()
    {
        Coloring();
        motherCube.Init();
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
    public void GridSwitching()
    {
        grid.SetActive(!grid.activeSelf);
        gridButtonText.text = grid.activeSelf ? "ON" : "OFF";
    }
}
