using UnityEngine;

public class Movement : StateMachineBehaviour
{
    //이 변수는 MovementObject에서 할당해주어야 합니다.
    public MovementObject movementObject { get; set; }

    public bool moveAble;
    public bool rotateAble;
    public bool jumpAble;
    public bool attackAble;
    public bool tumbleAble;
    [SerializeField]
    AudioClip clip;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SetMovementAble();
        if (clip != null)
            movementObject.PlayClip(clip);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SetMovementAble();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        movementObject.moveAble = true;
        movementObject.jumpAble = true;
        movementObject.attackAble = true;
        movementObject.tumbleAble = true;
        movementObject.rotateAble = true;
    }

    void SetMovementAble()
    {
        movementObject.moveAble = moveAble;
        movementObject.jumpAble = jumpAble;
        movementObject.attackAble = attackAble;
        movementObject.tumbleAble = tumbleAble;
        movementObject.rotateAble = rotateAble;
    }

}
