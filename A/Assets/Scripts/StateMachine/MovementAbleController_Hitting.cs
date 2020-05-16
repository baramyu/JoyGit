using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAbleController_Hitting : MovementAbleController
{
    List<Collider> hitedColliders;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        hitedColliders = new List<Collider>();
        SendHitMessage();
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SendHitMessage();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        SendHitMessage();

    }


    void SendHitMessage()
    {
        foreach (Collider hitCollider in movementObjectController.GetCollidersInHitBox())
        {
            if (!hitedColliders.Contains(hitCollider))
            {
                hitedColliders.Add(hitCollider);

                hitCollider.SendMessage("OnDamaged", movementObjectController.damage, SendMessageOptions.DontRequireReceiver);
            }
        }
    }

}
