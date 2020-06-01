using Cinemachine.Utility;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movement_Hit: Movement
{
    public int hitBoxNum;
    public float coefficient;
    public Vector3 knockbackForce;
    public bool down;
    public GameObject hitParticle;

    Collider hitBox;
    List<MovementObject> hitedObjList;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        hitBox = movementObject.hitBoxes[hitBoxNum];
        hitedObjList = new List<MovementObject>();
        Hit();
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        Hit();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);

        Hit();
    }


    void Hit()
    {
        Collider[] colliders;
        Vector3 knockbackForce = 
            movementObject.transform.right * this.knockbackForce.x + 
            movementObject.transform.up * this.knockbackForce.y + 
            movementObject.transform.forward * this.knockbackForce.z;


        colliders = Physics.OverlapBox(hitBox.bounds.center, hitBox.bounds.extents, Quaternion.identity, 1 << LayerMask.NameToLayer("MovementObject"));
        for(int i = 0; i < colliders.Length; i ++)
        {
            MovementObject movementObject = colliders[i].GetComponent<MovementObject>();
            if(!this.movementObject.CompareTag(movementObject.tag) && !hitedObjList.Contains(movementObject))
            {
                movementObject.OnKnockback(knockbackForce);
                movementObject.Twinkle(0.1f);
                movementObject.OnDamage((int)(this.movementObject.strength * coefficient), down);
                Instantiate(hitParticle, movementObject.transform.position, movementObject.transform.rotation);
                hitedObjList.Add(movementObject);
            }
        }
    }

}
