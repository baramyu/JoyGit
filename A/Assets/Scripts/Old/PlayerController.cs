using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerController : MovementObjectController
{
    
    public float moveSpeed;
    public float turnSpeed;
    public float jumpSpeed;
    public int jumpNum;
    public float tumbleSpeed;

    public int m_jumpNum { get; set; }

    public Vector3 dirInput { get; set; }
    public List<Interactive> interactObjList { get; set; }
    [SerializeField]
    GameObject dirGizmo;









    protected override void Start()
    {
        base.Start();
        interactObjList = new List<Interactive>();

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

        if(Input.GetKeyDown(SettingInfo.instance.attack))
        {
            Attack();
        }
        if(Input.GetKeyDown(SettingInfo.instance.tumble))
        {
            Tumble();
        }
        if(Input.GetKeyDown(SettingInfo.instance.interact))
        {
            Interact();
        }

#elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
        dirInput = VirtualJoystick.input;
        DirGizomo();
#endif

    }
    void FixedUpdate()
    {
        Moving();
        Rotating();
    }

    public void Interact()
    {
        if (GetInteractAbleObj() != null)
        {
            SoundManager.instance.PlayerButtonClick();
            GetInteractAbleObj().Interact();
        }
        else
        {
            SoundManager.instance.PlayerButtonHover();
        }

    }

    void DirGizomo()
    {
        if(dirInput != Vector3.zero)
        {
            dirGizmo.transform.position = transform.position + dirInput;
            dirGizmo.SetActive(true);
        }
        else
        {
            dirGizmo.SetActive(false);
        }
    }

    protected override void Moving()
    {
        if (!moveAble)
            return;

        m_Rigidbody.MovePosition(m_Rigidbody.position + dirInput * moveSpeed * Time.deltaTime);
        m_Animator.SetFloat("moveSpeed", dirInput.magnitude);
        m_Animator.SetFloat("fallingSpeed", m_Rigidbody.velocity.y);

    }
    protected override void Rotating()
    {
        if (!rotateAble)
            return;

        Vector3 desiredForwardd = Vector3.RotateTowards(transform.forward, dirInput, turnSpeed * Time.deltaTime, 0f);
        m_Rigidbody.rotation = Quaternion.LookRotation(desiredForwardd);
    }

    protected override void Jump()
    {
        if (!jumpAble)
            return;

        if (m_jumpNum > 0)
        {
            m_Rigidbody.velocity = new Vector3(m_Rigidbody.velocity.x, jumpSpeed, m_Rigidbody.velocity.z);
            //_Rigidbody.AddForce(Vector3.up * jumpSpeed * 10f);
            m_jumpNum--;
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
        else if(m_Animator.GetCurrentAnimatorStateInfo(0).IsTag("AfterDelay"))
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





    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.CompareTag("Ground") && collision.contacts[0].normal.y > 0.7f)
        {
            m_Animator.SetBool("onGround", true);
            m_jumpNum = jumpNum;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        m_Animator.SetBool("onGround", false);
        m_jumpNum = jumpNum - 1;
    }



    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Interactive"))
        {
            Interactive interactObj = other.GetComponent<Interactive>();

            if(!interactObjList.Contains(interactObj))
            {
                interactObjList.Add(interactObj);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Interactive"))
        {
            Interactive interactObj = other.GetComponent<Interactive>();

            if (interactObjList.Contains(interactObj))
            {
                interactObjList.Remove(interactObj);
            }
        }
    }


    public Interactive GetInteractAbleObj()
    {
        Interactive interactAbleObj = interactObjList.FindLast(interactObj => interactObj.InteractAble());

        return interactAbleObj;
    }
}
