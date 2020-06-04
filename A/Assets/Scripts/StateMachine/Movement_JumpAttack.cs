using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_JumpAttack : Movement
{
    public float time;

    Beta beta;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        beta = (Beta)movementObject;
        beta.ParabolaJump(time);
    }

}
