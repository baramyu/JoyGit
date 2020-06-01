using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MovementAbleController_Hitting : MovementAbleController
{
    public int hitBoxNum;

    Collider hitBox;
    List<Collider> hitedColliders;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        hitBox = movementObjectController.hitBoxes[hitBoxNum];
        hitedColliders = new List<Collider>();
        //SendHitMessage();
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //SendHitMessage();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        //SendHitMessage();

    }


    void Hit()
    {
        List<Collider> colliders = Physics.OverlapBox(hitBox.bounds.center, hitBox.bounds.extents, Quaternion.identity).ToList();
//        List<MovementObjectController> movementObjectControllers = colliders.FindAll(collider => collider.TryGetComponent<MovementObjectController>());


        foreach (Collider collider in colliders)
        {
            if (!hitedColliders.Contains(collider) && collider.gameObject != movementObjectController.gameObject)
            {
                hitedColliders.Add(collider);
            }
        }
    }


    /*void SendHitMessage()
    {
        foreach (Collider hitCollider in movementObjectController.GetCollidersInHitBox(hitBoxNum))
        {
            if (!hitedColliders.Contains(hitCollider))
            {
                hitedColliders.Add(hitCollider);
                hitCollider.SendMessage("OnDamage", movementObjectController.damage, SendMessageOptions.DontRequireReceiver);
            }
        }
    }*/

}
