using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class MonsterController : MovementObjectController
{
    public float maxMoveRadius;
    public float delayAttackTime;

    [SerializeField]
    NavMeshAgent m_Agent;
    [SerializeField]
    RectTransform m_Canvas;
    


    GameObject target;
    float lastSetDestinationTime;
    float delaySetDestinationTime;
    float lastAttackTime;
    GameObject[] players;


    protected override void Start()
    {
        base.Start();
        players = GameObject.FindGameObjectsWithTag("Player");

        SetRandomDestiantion();
    }

    private void Update()
    {
        m_Canvas.rotation = Quaternion.identity;

    }


    private void FixedUpdate()
    {
        Finding();
        Moving();

        if (IsAttackReady())
            Attack();
    }


    protected override void Moving()
    {

        if (!moveAble)
        {
            m_Agent.isStopped = true;
        }
        else
        {
            m_Agent.isStopped = false;
            m_Animator.SetFloat("moveSpeed", m_Agent.desiredVelocity.magnitude);

            if (target == null)
            {
                if(Time.time > lastSetDestinationTime + delaySetDestinationTime)
                {
                    SetRandomDestiantion();
                }
            }
            else
            {
                m_Agent.SetDestination(target.transform.position);
            }

        }


        

    }



    void SetRandomDestiantion()
    {
        Vector3 rndPoint = Random.insideUnitSphere * maxMoveRadius + transform.position;
        
        m_Agent.SetDestination(rndPoint);

        lastSetDestinationTime = Time.time;
        delaySetDestinationTime = Random.Range(0f, 5f);
    }


    



    void Finding()
    {

        if (target == null)
        {
            foreach (GameObject player in players)
            {
                if (Vector3.Distance(player.transform.position, transform.position) < maxMoveRadius)
                {
                    target = player;
                    transform.LookAt(target.transform);
                    m_Animator.SetTrigger("scream");
                    m_Agent.speed = 3f;

                    return;
                }
            }
        }

    }


    private bool IsAttackReady()
    {
        if (target == null)
            return false;
        if (Vector3.Distance(target.transform.position, transform.position) > 2f)
            return false;
        if (Time.time < lastAttackTime + delayAttackTime)
            return false;

        return true;
    }

    protected override void Attack()
    {
        if (!attackAble)
            return;

        m_Animator.SetTrigger("attack");

        lastAttackTime = Time.time;
    }



}
