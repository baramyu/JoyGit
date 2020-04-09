using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonCube : MonoBehaviour
{
    public float shotPower;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shot()
    {
        rb.constraints = RigidbodyConstraints.None;
        rb.AddForce(Vector3.down * shotPower);
    }
}
