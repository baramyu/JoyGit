using System.Collections;
using UnityEngine;

public class Trap2 : MonoBehaviour
{
    [SerializeField]
    Door door;
    [SerializeField]
    GameObject doorCam;
    [SerializeField]
    GameObject sword;

    bool isOperate;



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && sword == null && !isOperate)
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
        door.UnLock();
        yield return new WaitForSeconds(0.5f);
        doorCam.SetActive(false);
    }


}
