using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
    waiting,
    gaming
}
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameState gameState;
    public AudioSource bgmAudioSource;
    public AudioSource[] soundAudioSource;
    public float settlingSpeedLimit;
    public float settlingAngleLimit;
    public Color cubeColor;
    public Color coreColor;
    public Material cubeMaterial;
    public Material coreMaterial;
    public Image startButtonImage;
    public Image optionButtonImage;
    public Image rankingButtonImage;
    public Image tutorialButtonImage;
    public MotherCube motherCube;
    public GameObject gridObj;
    public Text gridButtonText;
    public Player player;
    private float time;
    private float best;
    private bool bestParticleReady;
    public TextMesh timeText;
    public TextMesh bestText;
    public float firstLostTime;
    public float decreaseLostTime;
    public float decreaseTerm;
    public GameObject uiPanel;
    public Camera letterBox;
    public Image optionPanelImage;
    public Text clapText;
    public Rect screen { set; get; }


    void Awake()
    {
        SetAspect(Screen.height, Screen.width);
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Coloring();
        GetOptionInfo();
        best = PlayerPrefs.GetFloat("best");
        bestText.text = "BEST: " + best.ToString("N2");
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState == GameState.gaming)
        {
            time += Time.deltaTime;
            timeText.text = "TIME: " + time.ToString("N2");

            if(time > best)
            {
                best = time;
                bestText.text = "BEST: " + best.ToString("N2");
                if(bestParticleReady)
                {
                    motherCube.Clap();
                    clapText.gameObject.SetActive(true);
                    StartCoroutine(Disable(clapText.gameObject, 3f));
                    bestParticleReady = false;
                }
            }

        }
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
        rankingButtonImage.color = cubeColor;
        tutorialButtonImage.color = cubeColor;
        clapText.color = cubeColor;
    }
    public void GameStart()
    {
        Coloring();
        motherCube.fullSonCube();
        motherCube.lostTime = firstLostTime;
        time = 0f;
        uiPanel.SetActive(false);
        bestParticleReady = true;
        motherCube.Clap();
        gameState = GameState.gaming;
        StartCoroutine("DifficultyUp", decreaseTerm);
    }
    IEnumerator DifficultyUp(float term)
    {
        while(true)
        {
            yield return new WaitForSeconds(term);


            float newLostTime = motherCube.lostTime - decreaseLostTime;

            if (newLostTime <= 0f)
            {
                newLostTime = motherCube.lostTime / 2f;
            }

            motherCube.lostTime = newLostTime;
            Coloring();
        }
    }
    IEnumerator Disable(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        obj.SetActive(false);
    }
    public void GameOver()
    {
        if(time == best)
        {
            PlayerPrefs.SetFloat("best", best);
        }

        uiPanel.SetActive(true);
        StopCoroutine("DifficultyUp");
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
        foreach (AudioSource audioSource in soundAudioSource)
        {
            audioSource.volume = soundSlider.value;
        }
        soundValueText.text =  (soundSlider.value * 100).ToString("N0");
        soundAudioSource[0].Play();
    }
    public void SensChange()
    {
        player.SetSwipeSensitivity(sensSlider.value);
        sensValueText.text = (sensSlider.value * 100).ToString("N0");
    }
    public void GridSwitching()
    {
        gridObj.SetActive(!gridObj.activeSelf);
        gridButtonText.text = gridObj.activeSelf ? "ON" : "OFF";
    }



    /// <summary>
    /// 레터박스를 통해 원하는 화면 비율을 세팅해주는 함수입니다.
    /// </summary>
    /// <param name="wRatio">원하는 가로 비율</param>
    /// <param name="hRatio">원하는 세로 비율</param>
    void SetAspect(float wRatio, float hRatio)
    {
        //메인 카메라의 비율 변경을 위해 받아옵니다.
        Camera mainCam = Camera.main;
        
        //새로운 화면 크기 0f~1f의 값을 가집니다.
        float newScreenWidth;
        float newScreenHeight;
        //레터박스의 크기 0f~1f의 값을 가집니다.
        float letterWidth;
        float letterHeight;
        //레터박스. 레터박스는 화면을 렌더하지않는 카메라 프리팹입니다.
        Camera letterBox1 = Instantiate(letterBox);
        Camera letterBox2 = Instantiate(letterBox);

        //가로가 더 긴 비율을 원하는 경우. 상하로 레터박스가 생깁니다.
        if (wRatio > hRatio)
        {
            //새로운 화면의 가로 크기는 화면의 최대치
            newScreenWidth = 1f;
            //세로 크기는 가로를 기준으로 비율을 맞춰줍니다(newScreenWidth : newScreenHeight = wRatio : hRatio)
            newScreenHeight = newScreenWidth / wRatio * hRatio;

            //레터박스의 가로 크기는 새로운 화면의 크기와 같습니다.
            letterWidth = newScreenWidth;
            //세로 크기는 원래 화면 크기(1f)에서 새로운 화면 크기를 뺀 크기입니다. 레터박스가 위, 아래 두곳에서 생기므로 2로 나누어줍니다.
            letterHeight = (1f - newScreenHeight) * 0.5f;

            //camera.rect는 왼쪽 아래부분이 0f,0f입니다.
            //새로운 크기의 화면을 할당해줍니다. 화면의 시작점x는 0, y는 아래 레터박스의 위부터입니다.
            mainCam.rect = new Rect(0f, letterHeight, newScreenWidth, newScreenHeight);

            letterBox1.rect = new Rect(0f, 0f, letterWidth, letterHeight);//아래 레터박스
            letterBox2.rect = new Rect(0f, 1f - letterHeight, letterWidth, letterHeight);//위 레터박스
        }
        //세로가 더 긴 비율을 원하는 경우. 좌우로 레터박스가 생깁니다. 나머지는 비슷합니다.
        else
        {
            newScreenHeight = 1f;
            newScreenWidth = newScreenHeight / hRatio * wRatio;

            letterHeight = newScreenHeight;
            letterWidth = (1f - newScreenWidth) * 0.5f;


            mainCam.rect = new Rect(letterWidth, 0f, newScreenWidth, newScreenHeight);

            letterBox1.rect = new Rect(0f, 0f, letterWidth, letterHeight);//왼쪽 레터박스
            letterBox2.rect = new Rect(1f - letterWidth, 0f, letterWidth, letterHeight);//오른쪽 레터박스
        }
        letterBox1.transform.parent = mainCam.transform;
        letterBox2.transform.parent = mainCam.transform;
        screen = new Rect(
            Camera.main.rect.x * Screen.width, 
            Camera.main.rect.y * Screen.height, 
            Camera.main.rect.width * Screen.width, 
            Camera.main.rect.height * Screen.height);
    }
    

    public void SetOptionInfo()
    {
        PlayerPrefs.SetInt("grid", gridObj.activeSelf? 1 : 0);
        PlayerPrefs.SetFloat("bgm", bgmSlider.value);
        PlayerPrefs.SetFloat("sound", soundSlider.value);
        PlayerPrefs.SetFloat("sens", sensSlider.value);
    }


    void GetOptionInfo()
    {
        bool grid = PlayerPrefs.GetInt("grid", 1) == 1? true : false;
        float bgm = PlayerPrefs.GetFloat("bgm", 0.5f);
        float sound = PlayerPrefs.GetFloat("sound", 0.5f);
        float sens = PlayerPrefs.GetFloat("sens", 0.15f);


        bgmSlider.value = bgm;
        soundSlider.value = sound;
        sensSlider.value = sens;
        
        BgmVolumeChange();
        SoundVolumeChange();
        SensChange();
        gridObj.SetActive(grid);
        gridButtonText.text = grid ? "ON" : "OFF";
    }
}
