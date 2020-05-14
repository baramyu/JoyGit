﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurricaneKick : MovementAbleController
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        playerController.m_Rigidbody.constraints |=  RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationY;
    }


    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);

        playerController.m_Rigidbody.constraints ^= RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationY;
    }
}
