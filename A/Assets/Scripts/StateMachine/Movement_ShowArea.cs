using Cinemachine.Utility;
using System.Collections.Generic;
using UnityEngine;

public class Movement_ShowArea : Movement
{
    public int areaNum;


    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        movementObject.areaSliders[areaNum].value = 0f;
        movementObject.areaSliders[areaNum].gameObject.SetActive(true);
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        movementObject.areaSliders[areaNum].value = stateInfo.normalizedTime;
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);

        movementObject.areaSliders[areaNum].gameObject.SetActive(false);
    }

}
