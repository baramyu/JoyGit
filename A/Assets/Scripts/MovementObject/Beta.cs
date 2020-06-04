using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Beta : MovementObject
{

    NavMeshAgent m_Agent;


    public float tryAttackRange;
    public float attackDelay;
    float attackTime;

    GameObject player;

    [SerializeField]
    GameObject jumpAttackImage;

    bool isApear;

    protected override void Start()
    {
        base.Start();
        m_Agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");
        m_Agent.speed = moveSpeed;
        m_Agent.angularSpeed = turnSpeed;

        jumpAttackImage.transform.parent = null;
    }
    private void FixedUpdate()
    {
        OnApear();
        if (!isApear)
            return;

        m_Animator.SetFloat("moveSpeed", m_Agent.desiredVelocity.magnitude / moveSpeed);
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

        if (Vector3.Distance(transform.position, player.transform.position) >= tryAttackRange)
        {
            m_Agent.isStopped = false;
            m_Agent.SetDestination(player.transform.position);
        }
        else
        {
            m_Agent.isStopped = true;
        }


    }
    protected override void Attack()
    {
        if (!attackAble)
            return;
        if (Time.time < attackTime + attackDelay)
            return;

        int pattern;//0:근거리 1:점프 2: 소환

        if (Vector3.Distance(transform.position, player.transform.position) <= tryAttackRange)
        {
            //가까이
            pattern = Random.Range(0, 3);
        }
        else
        {
            //멀리
            pattern = Random.Range(1, 3);
        }

        switch (pattern)
        {
            case 0:
                m_Animator.SetTrigger("swing");
                break;
            case 1:
                transform.LookAt(player.transform);
                m_Animator.SetTrigger("jumpAttack");
                break;
            case 2:
                transform.LookAt(player.transform);
                m_Animator.SetTrigger("fire");
                break;
        }

        attackTime = Time.time;
    }
    #endregion





    

    public void ParabolaJump(float time)
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        float verSpeed = -Physics.gravity.y * time / 2f;
        float horSpeed = distance / time;


        m_Agent.updatePosition = false;
        m_Agent.updateRotation = false;
        jumpAttackImage.transform.position = player.transform.position + Vector3.up * 0.01f;
        jumpAttackImage.SetActive(true);



        m_Rigidbody.velocity = Vector3.zero;
        m_Rigidbody.velocity = (player.transform.position - transform.position).normalized * horSpeed + Vector3.up * verSpeed;
    }

    void ParabolaJump(float time, Vector3 goal)
    {
        float distance = Vector3.Distance(transform.position, goal);
        float verSpeed = -Physics.gravity.y * time / 2f;
        float horSpeed = distance / time;


        m_Agent.updatePosition = false;
        m_Agent.updateRotation = false;
        jumpAttackImage.transform.position = player.transform.position + Vector3.up * 0.01f;
        jumpAttackImage.SetActive(true);



        m_Rigidbody.velocity = Vector3.zero;
        m_Rigidbody.velocity = (goal - transform.position).normalized * horSpeed + Vector3.up * verSpeed;
    }

    public override void OnKnockback(Vector3 force)
    {
        m_Agent.updatePosition = false;
        m_Agent.updateRotation = false;
        m_Rigidbody.AddForce(force);
    }
    public override void OnDamage(int damage, bool down)
    {
        curHp -= damage;
        hpSlider.value = (float)curHp / maxHp;

        if (curHp <= 0)
        {
            OnDie();
        }
        else
        {
            //몇 이상의 데미지만 넉백
        }
    }

    public void OnApear()
    {
        m_Animator.SetTrigger("sream");
        isApear = true;
    }

    public override void OnDie()
    {
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            jumpAttackImage.SetActive(false);
        }
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
