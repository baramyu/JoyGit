using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class MonsterController : MovementObjectController
{
    public float maxMoveRadius;


    [SerializeField]
    NavMeshAgent m_Agent;
    [SerializeField]
    RectTransform m_Canvas;
    [SerializeField]
    Slider hpSlider;

    GameObject target;
    float lastSetDestinationTime;
    float delaySetDestinationTime;


    protected override void Start()
    {
        base.Start();

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


    public void OnDamage(int damage)
    {
        curHp -= damage;
        hpSlider.value = (float)curHp / maxHp;

        if(curHp <= 0)
        {
            OnDie();
        }
        else
        {
            m_Animator.SetTrigger("damage");
        }
    }
    void OnDie()
    {
        Collider[] colliders = GetComponents<Collider>();
        foreach(Collider collider in colliders)
        {
            collider.enabled = false;
        }
        m_Animator.SetTrigger("die");
    }



    void Finding()
    {
        if (target != null)
            return;

        Collider[] colliders = Physics.OverlapSphere(transform.position, maxMoveRadius);

        foreach(Collider collider in colliders)
        {
            if(collider.CompareTag("Player"))
            {
                target = collider.gameObject;
                transform.LookAt(target.transform);
                m_Animator.SetTrigger("scream");
                m_Agent.speed = 3f;

                return;
            }
        }
    }


    protected override void Attack()
    {
        if (!attackAble)
            return;
        

    }


    private void OnTriggerEnter(Collider other)
    {
        if (target == null && other.CompareTag("Player"))
        {
            target = other.gameObject;
            transform.LookAt(target.transform);
            m_Animator.SetTrigger("scream");

            m_Agent.speed = 3f;
        }
    }


}
