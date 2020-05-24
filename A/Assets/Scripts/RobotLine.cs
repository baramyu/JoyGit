using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotLine : MonoBehaviour
{
    public Vector3 startPos;
    public float endtPoint;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * speed * Time.fixedDeltaTime);
        if (transform.position.x >= endtPoint)
        {
            transform.position = startPos;

        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
