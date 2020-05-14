using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    public float maxMoveRadius;
    public float damage;
    public float attackDelay;

    [SerializeField]
    NavMeshAgent m_agent;
    [SerializeField]
    Animator m_animator;

    float curTime;
    float waitTime;
    GameObject target;

    void Start()
    {
        SetRandomDestination();
    }

    void Update()
    {
        m_animator.SetFloat("moveSpeed", m_agent.desiredVelocity.magnitude * 10f);
        if (target == null && m_agent.velocity.magnitude == 0f)
        {
            curTime += Time.deltaTime;

            if (curTime > waitTime)
            {
                SetRandomDestination();
                curTime = 0f;
                waitTime = Random.Range(1f, 2f);
            }
        }

    }



    void SetRandomDestination()
    {
        Vector3 randomDirection = Random.insideUnitSphere * maxMoveRadius;

        m_agent.SetDestination(randomDirection);
    }


    void Attack()
    {

    }


    private void OnTriggerEnter(Collider other)
    {
        if (target == null && other.CompareTag("Player"))
        {
            target = other.gameObject;
            m_animator.SetTrigger("scream");
        }
    }


}
