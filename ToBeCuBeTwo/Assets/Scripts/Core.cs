using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    public MotherCube motherCube;
    public GameObject sonCube;
    public float settlingSpeedLimit;
    public float settlingAngleLimit;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void GetSonCube()
    {
        motherCube.AddFullCoreArray(this);
        sonCube.SetActive(true);
    }
    public void LostSonCube()
    {
        sonCube.SetActive(false);
    }


    
    void OnTriggerStay(Collider other)
    {
        //아들큐브가 없을때 아들큐브와 충돌하면
        if(sonCube.activeSelf == false && other.transform.CompareTag("SonCube"))
        {
            Debug.Log(other.attachedRigidbody.velocity.magnitude);
            //속도, 각도가 제한 범위 내에 있으면
            if (other.attachedRigidbody.velocity.magnitude <= settlingSpeedLimit)
            {
                Destroy(other.gameObject);
                GetSonCube();
            }
        }
    }

}
