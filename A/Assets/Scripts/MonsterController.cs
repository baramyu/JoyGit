using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MovementObjectController
{
    public int hp;
    public float maxMoveRadius;


    [SerializeField]
    NavMeshAgent m_agent;

    GameObject target;
    bool dead;
    float lastSetDestinationTime;
    float delaySetDestinationTime;


    protected override void Start()
    {
        base.Start();

        SetRandomDestiantion();
    }

    private void FixedUpdate()
    {
        Moving();
    }


    protected override void Moving()
    {

        if (!moveAble)
        {
            m_agent.isStopped = true;
        }
        else
        {
            m_agent.isStopped = false;
            m_Animator.SetFloat("moveSpeed", m_agent.desiredVelocity.magnitude);

            if (target == null)
            {
                if(Time.time > lastSetDestinationTime + delaySetDestinationTime)
                {
                    SetRandomDestiantion();
                }
            }
            else
            {
                m_agent.SetDestination(target.transform.position);
            }
        }


        

    }



    void SetRandomDestiantion()
    {
        Vector3 rndPoint = Random.insideUnitSphere * maxMoveRadius + transform.position;
        
        m_agent.SetDestination(rndPoint);

        lastSetDestinationTime = Time.time;
        delaySetDestinationTime = Random.Range(0f, 5f);
    }


    void OnDamaged(int damage)
    {
        hp -= damage;

        if(hp <= 0)
        {
            OnDie();
        }
        else
        {
            m_Animator.SetTrigger("damaged");
        }
    }
    void OnDie()
    {
        m_Animator.SetTrigger("die");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (target == null && other.CompareTag("Player"))
        {
            target = other.gameObject;
            transform.LookAt(target.transform);
            m_Animator.SetTrigger("scream");

            m_agent.speed = 3f;
        }
    }


}
