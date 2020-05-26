using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.MemoryProfiler;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }


    [SerializeField]
    GameObject titleMenu;
    [SerializeField]
    CinemachineBrain brainCam;
    [SerializeField]
    CinemachineVirtualCamera mainFollowCam;
    [SerializeField]
    CinemachineVirtualCamera titleCam;
    [SerializeField]
    CinemachineVirtualCamera introCam;
    [SerializeField]
    CinemachineDollyCart introCart;
    [SerializeField]
    CinemachineSmoothPath introPath;
    [SerializeField]
    GameObject[] explosions;
    [SerializeField]
    GameObject robotLine;
    [SerializeField]
    GameObject[] robots;

    public void StartGame()
    {
        StartCoroutine(Opening());

    }
    IEnumerator Opening()
    {
        titleMenu.SetActive(false);
        titleCam.gameObject.SetActive(false);
        introCam.gameObject.SetActive(true);
        yield return new WaitForSeconds(brainCam.m_DefaultBlend.m_Time);
        introCart.m_Speed = 0.3f;
        yield return new WaitForSeconds(introPath.PathLength / introCart.m_Speed);
        introCam.gameObject.SetActive(false);
        robotLine.SetActive(false);
        mainFollowCam.gameObject.SetActive(true);
        SoundManager.instance.FadeOutBgm(brainCam.m_DefaultBlend.m_Time);
        yield return new WaitForSeconds(brainCam.m_DefaultBlend.m_Time);
        yield return new WaitForSeconds(1f);
        for(int i = 0; i < 3; i++)
        {
            mainFollowCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 2f;
            mainFollowCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 2f;
            explosions[i].gameObject.SetActive(true);
            Destroy(explosions[i], 1f);

            robots[i].SetActive(true);
            robots[i].GetComponentInChildren<Rigidbody>().AddForce(robots[i].transform.forward * 30000f);

            yield return new WaitForSeconds(0.2f);
        }
        mainFollowCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0f;
        mainFollowCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0f;
    }

}
