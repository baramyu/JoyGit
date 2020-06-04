using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_Fire : Movement
{
    public int spawnPivotNum;
    [SerializeField]
    GameObject bullet;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        Instantiate(bullet, movementObject.pivots[spawnPivotNum].position, movementObject.transform.rotation);
    }

}
