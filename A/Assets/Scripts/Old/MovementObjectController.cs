using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class MovementObjectController : MonoBehaviour
{
    public int maxHp;
    public int damage;
    public Rigidbody m_Rigidbody;
    public Collider[] hitBoxes;

    [SerializeField]
    protected Animator m_Animator;
    
    public Slider hpSlider;


    protected int curHp;

    public bool isDie { get; set; }
    public bool moveAble { get; set; }
    public bool tumbleAble { get; set; }
    public bool jumpAble { get; set; }
    public bool attackAble { get; set; }
    public bool rotateAble { get; set; }


    protected virtual void Start()
    {
        curHp = maxHp;

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

    public virtual void OnDamage(int damage)
    {
        curHp -= damage;
        hpSlider.value = (float)curHp / maxHp;

        if (curHp <= 0)
        {
            OnDie();
        }
        else
        {
            m_Animator.SetTrigger("damage");
        }
    }

    protected virtual void OnDie()
    {
        Collider[] colliders = GetComponents<Collider>();


        if (m_Rigidbody != null)
        {
            m_Rigidbody.isKinematic = true;
            m_Rigidbody.detectCollisions = false;
        }
        foreach (Collider collider in colliders)
        {
            collider.enabled = false;
        }



        isDie = true;
        m_Animator.SetBool("die", true);
    }



    /*public Collider[] GetCollidersInHitBox(int hitBoxNum)
    {
        Collider[] hitColliders = Physics.OverlapBox(hitBoxes[hitBoxNum].bounds.center, hitBoxes[hitBoxNum].bounds.extents, Quaternion.identity, ~(1 << gameObject.layer));
        return hitColliders;
    }*/

    

}
