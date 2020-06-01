using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField]
    Door door;
    [SerializeField]
    Door swordRoomDoor;
    [SerializeField]
    GameObject swordRoomDoorCam;
    [SerializeField]
    MovementObject[] enemyType;
    List <MovementObject> enemys;
    [SerializeField]
    GameObject[] spawnPoints;
    public float spawnDelay;
    public int spawnNum;

    bool isOperate;

    private void Start()
    {
        int spawnCnt = 0;
        int enemyRnd;
        int spawnRnd;
        enemys = new List<MovementObject>();

        while (spawnCnt < spawnNum)
        {
            enemyRnd = Random.Range(0, enemyType.Length);
            spawnRnd = Random.Range(1, 101);
            spawnRnd = spawnRnd < 70 ? 0 : 1;

            enemys.Add(Instantiate(enemyType[enemyRnd], spawnPoints[spawnRnd].transform.position, Quaternion.identity));
            enemys[spawnCnt].gameObject.SetActive(false);



            spawnCnt++;
        }
        
    }
    private void Update()
    {
        if(isOperate && enemys.FindAll(enemy => enemy == null).Count == spawnNum)
        {
            StartCoroutine(SwordRoomDoorUnLock());
            enabled = false;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player") && !isOperate)
        {
            isOperate = true;
            door.Close(0.5f, true);
            StartCoroutine(EnemySpawnCor());
        }
    }

    IEnumerator EnemySpawnCor()
    {
        for(int i = 0; i < enemys.Count; i++)
        {
            enemys[i].gameObject.SetActive(true);
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    IEnumerator SwordRoomDoorUnLock()
    {
        swordRoomDoorCam.SetActive(true);
        yield return new WaitForSeconds(GameManager.instance.brainCam.m_DefaultBlend.m_Time);
        yield return new WaitForSeconds(0.5f);
        swordRoomDoor.UnLock();
        yield return new WaitForSeconds(0.5f);
        swordRoomDoorCam.SetActive(false);
    }

}
