using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour
{
    public float hoverLength;
    public float hoveSpeed;

    float originY;
    int dir;

    void Start()
    {
        originY = transform.position.y;
        dir = 1;
    }

    void Update()
    {
        transform.Translate(dir * Vector3.up * hoveSpeed * Time.deltaTime);

        if (transform.position.y > originY + hoverLength)
            dir = -1;
        else if(transform.position.y < originY - hoverLength)
            dir = 1;
    }
}
