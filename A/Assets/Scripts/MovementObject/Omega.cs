using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Omega : MovementObject
{
    public float tryAttackRange;
    public float explosionTime;
    public float explosionRadius;
    public float explosionForce;
    

    [SerializeField]
    NavMeshAgent m_Agent;

    [SerializeField]
    GameObject apearParticle;
    [SerializeField]
    GameObject destroyParticle;
    [SerializeField]
    GameObject explosionParticle;

    GameObject target;



    protected override void Start()
    {
        base.Start();

        target = GameObject.FindWithTag("Player");

        m_Agent.speed = moveSpeed;
        m_Agent.angularSpeed = turnSpeed;

        //등장
        Instantiate(apearParticle, transform);


    }
    private void FixedUpdate()
    {
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
        
        m_Agent.SetDestination(target.transform.position);

    }
    protected override void Attack()
    {
        if (!attackAble)
            return;

        if (Vector3.Distance(transform.position, target.transform.position) <= tryAttackRange)
        {
            StartCoroutine(ExplosionCor());
            Jump();
        }

    }
    protected override void Jump()
    {
        if (!jumpAble)
            return;

        if (curJumpNum > 0)
        {
            m_Animator.SetTrigger("jump");
            m_Agent.updatePosition = false;
            m_Agent.updateRotation = false;
            m_Rigidbody.AddForce((transform.up + transform.forward) * jumpSpeed);

            curJumpNum--;
        }
    }
    #endregion

    public override void OnKnockback(Vector3 force)
    {
        m_Agent.updatePosition = false;
        m_Agent.updateRotation = false;
        m_Rigidbody.AddForce(force);
    }
    public override void OnDamage(int damage, bool down)
    {
        base.OnDamage(damage, down);
    }

    public override void OnDie()
    {
        Instantiate(destroyParticle, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    IEnumerator ExplosionCor()
    {
        yield return new WaitForSeconds(explosionTime);

        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        for(int i = 0; i < colliders.Length; i++)
        {
            if(colliders[i].TryGetComponent(out Rigidbody rigidbody))
                rigidbody.AddExplosionForce(explosionForce, transform.position, explosionRadius);

            if (colliders[i].TryGetComponent(out MovementObject movementObject))
                movementObject.OnDamage(strength, true);
        }


        Instantiate(explosionParticle, transform.position, transform.rotation);

        Destroy(gameObject);
    }


    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.CompareTag("Ground") && collision.contacts[0].normal.y > 0.7f)
        {
            m_Animator.SetBool("onGround", true);
            curJumpNum = maxJumpNum;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            m_Animator.SetBool("onGround", false);
            curJumpNum = maxJumpNum - 1;
        }
    }
}
