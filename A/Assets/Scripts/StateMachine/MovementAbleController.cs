using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAbleController : StateMachineBehaviour
{
    public PlayerController playerController { get; set; }

    public bool moveAble;
    public bool rotateAble;
    public bool jumpAble;
    public bool attackAble;
    public bool tumbleAble;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerController.moveAble = moveAble;
        playerController.jumpAble = jumpAble;
        playerController.attackAble = attackAble;
        playerController.tumbleAble = tumbleAble;
        playerController.rotateAble = rotateAble;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerController.moveAble = moveAble;
        playerController.jumpAble = jumpAble;
        playerController.attackAble = attackAble;
        playerController.tumbleAble = tumbleAble;
        playerController.rotateAble = rotateAble;
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerController.moveAble = true;
        playerController.jumpAble = true;
        playerController.attackAble = true;
        playerController.tumbleAble = true;
        playerController.rotateAble = true;
    }
}
