using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.Animations;
using UnityEngine;

public class Player : MovementObject
{
    //이동 관련
    [SerializeField]
    GameObject dirGizmo;
    Vector3 dirInput;

    //interact관련
    public List<IInteract> onStayInteractList { get; set; }

    //sword
    [SerializeField]
    AnimatorController swordAnimator;
    [SerializeField]
    GameObject sword;

    //gun
    [SerializeField]
    GameObject gun;
    [SerializeField]
    ParticleSystem gunPointParticle;

    protected override void Start()
    {
        base.Start();
        onStayInteractList = new List<IInteract>();
    }
    private void Update()
    {
#if UNITY_STANDALONE || UNITY_WEBPLAYER
        dirInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        dirInput = dirInput.normalized;

        if (Input.GetKeyDown(SettingInfo.instance.jump))
        {
            Jump();
        }

        if (Input.GetKeyDown(SettingInfo.instance.attack))
        {
            Attack();
        }
        if (Input.GetKeyDown(SettingInfo.instance.tumble))
        {
            Tumble();
        }
        if (Input.GetKeyDown(SettingInfo.instance.interact))
        {
            Interact();
        }
        if(gun.activeSelf == true && Input.GetKeyDown(SettingInfo.instance.fire))
        {
            Fire();
        }

#elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
        dirInput = VirtualJoystick.input;
        DirGizomo();
#endif
    }
    private void FixedUpdate()
    {
        m_Animator.SetFloat("moveSpeed", dirInput.magnitude);
        m_Animator.SetFloat("fallingSpeed", m_Rigidbody.velocity.y);

        Move();
        Rotate();
    }


    #region Movemnet
    protected override void Move()
    {
        if (!moveAble)
            return;
        m_Rigidbody.MovePosition(m_Rigidbody.position + dirInput * moveSpeed * Time.fixedDeltaTime);
    }
    protected override void Rotate()
    {
        if (!rotateAble)
            return;
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, dirInput, turnSpeed * Time.fixedDeltaTime, 0f);
        m_Rigidbody.rotation = Quaternion.LookRotation(desiredForward);
    }
    protected override void Jump()
    {
        if (!jumpAble)
            return;
        if (curJumpNum > 0)
        {
            m_Rigidbody.velocity = new Vector3(m_Rigidbody.velocity.x, jumpSpeed, m_Rigidbody.velocity.z);
            curJumpNum--;
        }
    }
    protected override void Attack()
    {
        if (!attackAble)
            return;

        if (m_Animator.GetCurrentAnimatorStateInfo(0).IsTag("Idle"))
        {
            m_Animator.SetTrigger("attack");
        }
        else if (m_Animator.GetCurrentAnimatorStateInfo(0).IsTag("AfterDelay"))
        {
            m_Animator.SetTrigger("combo");
        }
    }
    protected override void Tumble()
    {
        if (!tumbleAble)
            return;

        Vector3 tmpYSpeed = Vector3.up * m_Rigidbody.velocity.y;

        if (dirInput == Vector3.zero)
        {
            m_Animator.SetTrigger("tumble");
            m_Rigidbody.velocity = transform.forward * tumbleSpeed + tmpYSpeed;
        }
        else
        {
            transform.rotation = Quaternion.LookRotation(dirInput);
            m_Animator.SetTrigger("tumble");
            m_Rigidbody.velocity = dirInput.normalized * tumbleSpeed + tmpYSpeed;
        }
    }
    void Fire()
    {
        if (!attackAble)
            return;
        m_Animator.SetTrigger("fire");
        gunPointParticle.Play();

    }
    #endregion




    void DirGizomo()
    {
        if (dirInput != Vector3.zero)
        {
            dirGizmo.transform.position = transform.position + dirInput;
            dirGizmo.SetActive(true);
        }
        else
        {
            dirGizmo.SetActive(false);
        }
    }
    void Interact()
    {
        IInteract interactAbleObj = onStayInteractList.FindLast(interactObj => interactObj.IsInteractAble());

        if(interactAbleObj != null)
        {
            SoundManager.instance.PlayerButtonClick();
            interactAbleObj.Interact();
        }
        else
        {
            SoundManager.instance.PlayerButtonHover();
        }
    }

    public void GetSword()
    {
        m_Animator.runtimeAnimatorController = swordAnimator;
        sword.SetActive(true);
        InitMovement();
    }
    public  void GetGun()
    {
        gun.SetActive(true);
    }


    #region OnCollision
    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.CompareTag("Ground") && collision.contacts[0].normal.y > 0.7f)
        {
            m_Animator.SetBool("onGround", true);
            curJumpNum = maxJumpNum;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if(collision.transform.CompareTag("Ground"))
        {
            m_Animator.SetBool("onGround", false);
            curJumpNum = maxJumpNum - 1;
        }
    }
    #endregion

    #region OnTrigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interact"))
        {
            IInteract interactObj = other.GetComponent<IInteract>();

            if (!onStayInteractList.Contains(interactObj))
            {
                onStayInteractList.Add(interactObj);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Interact"))
        {
            IInteract interactObj = other.GetComponent<IInteract>();

            if (onStayInteractList.Contains(interactObj))
            {
                onStayInteractList.Remove(interactObj);
            }
        }
    }
    #endregion

}
