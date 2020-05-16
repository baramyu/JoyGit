using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAbleController : StateMachineBehaviour
{
    public MovementObjectController movementObjectController { get; set; }

    public bool moveAble;
    public bool rotateAble;
    public bool jumpAble;
    public bool attackAble;
    public bool tumbleAble;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        movementObjectController.moveAble = moveAble;
        movementObjectController.jumpAble = jumpAble;
        movementObjectController.attackAble = attackAble;
        movementObjectController.tumbleAble = tumbleAble;
        movementObjectController.rotateAble = rotateAble;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        movementObjectController.moveAble = moveAble;
        movementObjectController.jumpAble = jumpAble;
        movementObjectController.attackAble = attackAble;
        movementObjectController.tumbleAble = tumbleAble;
        movementObjectController.rotateAble = rotateAble;
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        movementObjectController.moveAble = true;
        movementObjectController.jumpAble = true;
        movementObjectController.attackAble = true;
        movementObjectController.tumbleAble = true;
        movementObjectController.rotateAble = true;
    }
}
