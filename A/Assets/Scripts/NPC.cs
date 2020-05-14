using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    public Vector3 minPoint;
    public Vector3 maxPoint;
    
    NavMeshAgent m_agent;
    Animator m_animator;
    RectTransform m_canvas;

    float curTime;
    float waitTime;

    void Start()
    {
        m_agent = GetComponent<NavMeshAgent>();
        m_animator = GetComponent<Animator>();
        m_canvas = GetComponentInChildren<RectTransform>();

        //waitTime = Random.Range(0f, 10f);
        SetRandomDestination();

    }
    //desiredVelocity

    void Update()
    {
        m_canvas.rotation = Quaternion.identity;
        m_animator.SetFloat("moveSpeed", m_agent.desiredVelocity.magnitude);
        if(m_agent.desiredVelocity.magnitude == 0f)
        {
            curTime += Time.deltaTime;

            if(curTime > waitTime)
            {
                m_agent.isStopped = false;
                SetRandomDestination();
                curTime = 0f;
                waitTime = Random.Range(0f, 10f);
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        transform.LookAt(other.transform);
        Action("action1");
    }




    void SetRandomDestination()
    {
        Vector3 rndVector = new Vector3(Random.Range(minPoint.x, maxPoint.x), Random.Range(minPoint.y, maxPoint.y), Random.Range(minPoint.z, maxPoint.z));


        m_agent.SetDestination(rndVector);
    }




    void Action(string action)
    {
        waitTime = 5f;
        m_agent.isStopped = true;
        m_animator.SetTrigger(action);
    }

}
