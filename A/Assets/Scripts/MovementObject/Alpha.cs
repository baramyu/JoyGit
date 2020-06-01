using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Alpha : MovementObject
{
    //find
    public float setDestinationDelay;
    public float maxDestinationRadius;
    public float findPlayerDistance;
    float lastSetDestinationTime;

    public float runSpeed;
    public float tryAttackRange;

    [SerializeField]
    NavMeshAgent m_Agent;

    [SerializeField]
    GameObject apearParticle;
    [SerializeField]
    GameObject destroyParticle;
    [SerializeField]
    GameObject deadBody;

    GameObject player;
    GameObject target;
    Vector3 lastKnockBack;

    protected override void Start()
    {
        base.Start();
        player = GameObject.FindWithTag("Player");
        m_Agent.speed = moveSpeed;
        m_Agent.angularSpeed = turnSpeed;

        //등장
        Instantiate(apearParticle, transform);
    }
    private void FixedUpdate()
    {
        m_Animator.SetFloat("moveSpeed", m_Agent.desiredVelocity.magnitude / runSpeed);
        m_Animator.SetFloat("fallingSpeed", m_Rigidbody.velocity.y);
        Move();
        Attack();
    }


    #region Movemnet
    protected override void Move()
    {
        if (!moveAble)
        {
            m_Agent.speed = 0f;
            return;
        }

        if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Move Blend Tree"))
        {
            m_Agent.nextPosition = transform.position;
            m_Agent.updatePosition = true;
            m_Agent.updateRotation = true;
        }

        if (target == null)
        {
            m_Agent.speed = moveSpeed;
            if (Time.time > lastSetDestinationTime + setDestinationDelay)
            {
                Vector3 rndPoint = Random.insideUnitSphere * maxDestinationRadius + transform.position;
                m_Agent.SetDestination(rndPoint);
                lastSetDestinationTime = Time.time;
            }
            if (Vector3.Distance(transform.position, player.transform.position) <= findPlayerDistance)
            {
                target = player;
            }

        }
        else
        {
            m_Agent.speed = runSpeed;
            m_Agent.SetDestination(target.transform.position);
        }


    }
    protected override void Attack()
    {
        if (!attackAble)
            return;

        if (target == null)
            return;

        if (Vector3.Distance(transform.position, target.transform.position) <= tryAttackRange)
        {
            if (m_Animator.GetCurrentAnimatorStateInfo(0).IsTag("Idle"))
            {
                m_Animator.SetTrigger("attack");
            }
            else if (m_Animator.GetCurrentAnimatorStateInfo(0).IsTag("AfterDelay"))
            {
                m_Animator.SetTrigger("combo");
            }
        }

    }
    #endregion

    public override void OnKnockback(Vector3 force)
    {
        m_Agent.updatePosition = false;
        m_Agent.updateRotation = false;
        m_Rigidbody.AddForce(force);
        lastKnockBack = force;
    }
    public override void OnDamage(int damage, bool down)
    {
        base.OnDamage(damage, down);
    }

    public override void OnDie()
    {
        Instantiate(destroyParticle, transform.position, transform.rotation);
        GameObject deadBody = Instantiate(this.deadBody, transform.position, transform.rotation);
        deadBody.GetComponentInChildren<Rigidbody>().AddForce(lastKnockBack * 2f);
        Destroy(deadBody, 5f);
        Destroy(gameObject);
    }




    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.CompareTag("Ground") && collision.contacts[0].normal.y > 0.7f)
        {
            m_Animator.SetBool("onGround", true);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            m_Animator.SetBool("onGround", false);
        }
    }
}
