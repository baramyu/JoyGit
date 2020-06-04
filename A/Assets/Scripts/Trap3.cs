using System.Collections;
using UnityEngine;

public class Trap3 : MonoBehaviour
{
    [SerializeField]
    Door door1;
    [SerializeField]
    Door door2;
    [SerializeField]
    GameObject doorCam;
    [SerializeField]
    GameObject gun;
    bool isOperate;



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && gun == null && !isOperate)
        {
            isOperate = true;
            StartCoroutine(DoorUnLockCor());
        }

    }


    IEnumerator DoorUnLockCor()
    {
        doorCam.SetActive(true);
        yield return new WaitForSeconds(GameManager.instance.brainCam.m_DefaultBlend.m_Time);
        yield return new WaitForSeconds(0.5f);
        door1.UnLock();
        door2.UnLock();
        yield return new WaitForSeconds(0.5f);
        doorCam.SetActive(false);
    }


}
