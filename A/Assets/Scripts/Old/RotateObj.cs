using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObj : MonoBehaviour
{
    public float speed;


    void Start()
    {
        
    }


    private void FixedUpdate()
    {
        transform.Rotate(Vector3.up * speed * Time.fixedDeltaTime);
    }
}
