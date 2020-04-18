using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonCube : MonoBehaviour
{


    public float minHeight;
    public Rigidbody rb;

    private void OnDisable()
    {
        rb.velocity = Vector3.zero;

        rb.angularVelocity = Vector3.zero;

        rb.Sleep();
    }

    private void OnEnable()
    {
        rb.WakeUp();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= minHeight)
            gameObject.SetActive(false);


        
    }


    public void Shot(float shotPower, Vector3 spawnPoint)
    {
        transform.position = spawnPoint;
        transform.rotation = Quaternion.identity;
        rb.AddForce(Vector3.down * shotPower);
    }
    
    public void Freeze()
    {
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }



}
