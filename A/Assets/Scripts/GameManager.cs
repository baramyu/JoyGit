using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }




    [SerializeField]
    CinemachineVirtualCamera titleCam;
    [SerializeField]
    CinemachineVirtualCamera introCam;
    [SerializeField]
    CinemachineDollyCart introCart;

    public void StartGame()
    {
    }
}
