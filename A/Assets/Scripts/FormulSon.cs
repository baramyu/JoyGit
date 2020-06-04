using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormulSon : MonoBehaviour
{
    Rigidbody m_Rigidbody;
    public float time;
    public GameObject goal;

    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.LookAt(goal.transform);

            //목표까지의 거리
            float distance = Vector3.Distance(transform.position, goal.transform.position);
            float verSpeed = -Physics.gravity.y * time / 2f;
            float horSpeed = distance / time;

            m_Rigidbody.velocity = Vector3.zero;
            m_Rigidbody.velocity = (goal.transform.position - transform.position).normalized * horSpeed + Vector3.up * verSpeed;
        }
    }
}
