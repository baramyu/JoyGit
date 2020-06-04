using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap4 : MonoBehaviour
{
    [SerializeField]
    GameObject door;
    [SerializeField]
    GameObject bossHpSlider;
    [SerializeField]
    GameObject bossCam;
    [SerializeField]
    Beta boss;

    bool isOperate;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isOperate)
        {
            door.SetActive(true);
            
        }
    }

    IEnumerator DoorUnLockCor()
    {
        bossCam.SetActive(true);
        yield return new WaitForSeconds(GameManager.instance.brainCam.m_DefaultBlend.m_Time);
        boss.OnApear();
        yield return new WaitForSeconds(1f);
        bossCam.SetActive(false);
        bossHpSlider.SetActive(true);
    }
}
