using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementObjectController : MonoBehaviour
{
    public int damage;

    public Rigidbody m_Rigidbody;

    [SerializeField]
    protected Animator m_Animator;
    [SerializeField]
    protected BoxCollider hitBox;

    public bool moveAble { get; set; }
    public bool tumbleAble { get; set; }
    public bool jumpAble { get; set; }
    public bool attackAble { get; set; }
    public bool rotateAble { get; set; }


    protected virtual void Start()
    {
        moveAble = true;
        jumpAble = true;
        attackAble = true;
        tumbleAble = true;
        rotateAble = true;

        MovementAbleController[] movementAbleControllers = m_Animator.GetBehaviours<MovementAbleController>();
        for (int i = 0; i < movementAbleControllers.Length; i++)
        {
            movementAbleControllers[i].movementObjectController = this;
        }
    }



    protected virtual void Moving()
    {
    }
    protected virtual void Rotating()
    {
    }

    protected virtual void Jump()
    {
    }

    protected virtual void Attack()
    {
    }
    protected virtual void Tumble()
    {
    }

    public Collider[] GetCollidersInHitBox()
    {
        Collider[] hitColliders = Physics.OverlapBox(hitBox.center + transform.position, hitBox.size / 2, Quaternion.identity, 1 << 9);

        return hitColliders;
    }

}
