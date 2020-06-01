using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingInfo : MonoBehaviour
{
    public static SettingInfo instance;

    float bgmVolume;
    float fxVolume;

    /*public KeyCode up;//defalt:w, 119
    public KeyCode down;//defalt:s, 115
    public KeyCode right;//defalt:d, 100
    public KeyCode left;//defalt:a, 97*/
    public KeyCode attack;//defalt:mouse0, 323
    public KeyCode tumble;//defalt:LShift, 304
    public KeyCode jump;//defalt:space, 32
    public KeyCode skill1;//defalt:1, 49
    public KeyCode skill2;//defalt:2, 50
    public KeyCode interact;//defalt:e, 101


    [SerializeField]
    InteractPanel interactPanel;

    void Awake()
    {
        instance = this;
        bgmVolume = PlayerPrefs.GetFloat("bgmVolume", 50f);
        fxVolume = PlayerPrefs.GetFloat("fxVolume", 50f);

#if UNITY_STANDALONE || UNITY_WEBPLAYER
        attack = (KeyCode)PlayerPrefs.GetInt("attackKey", 323);
        tumble = (KeyCode)PlayerPrefs.GetInt("tumbleKey", 304);
        jump = (KeyCode)PlayerPrefs.GetInt("jumpKey", 32);
        skill1 = (KeyCode)PlayerPrefs.GetInt("skill1Key", 49);
        skill2 = (KeyCode)PlayerPrefs.GetInt("skill2Key", 50);
        interact = (KeyCode)PlayerPrefs.GetInt("interatKey", 101);

#elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE

#endif
    }

    void Start()
    {
        SoundManager.instance.SetBgmVolume(bgmVolume);
        SoundManager.instance.SetFxVolume(fxVolume);




    }


    void Update()
    {
        
    }
}
