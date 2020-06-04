using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MovementObject : MonoBehaviour
{
    //행동관련
    protected Animator m_Animator;
    protected Rigidbody m_Rigidbody;
    protected AudioSource m_AudioSource;
    public bool moveAble { get; set; }
    public bool rotateAble { get; set; }
    public bool tumbleAble { get; set; }
    public bool jumpAble { get; set; }
    public bool attackAble { get; set; }
    public float moveSpeed;
    public float turnSpeed;
    public float jumpSpeed;
    public float tumbleSpeed;
    public int maxJumpNum;
    public int curJumpNum { get; set; }




    //hp관련
    [SerializeField]
    protected int maxHp;
    protected int curHp;
    [SerializeField]
    protected Slider hpSlider;

    //공격관련
    public Collider[] hitBoxes;
    public int strength;

    //효과관련
    public Transform[] pivots;
    public Slider[] areaSliders;
    protected Renderer[] m_Renderers;
    protected Material[] originalMaterials;
    [SerializeField]
    protected Material twinkleMaterial;

    protected virtual void Awake()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Renderers = GetComponentsInChildren<Renderer>();
        m_AudioSource = GetComponent<AudioSource>();
        curHp = maxHp;
    }

    protected virtual void Start()
    {
        originalMaterials = new Material[m_Renderers.Length];
        for(int i = 0; i < m_Renderers.Length; i++)
        {
            originalMaterials[i] = m_Renderers[i].material;
        }



        moveAble = true;
        jumpAble = true;
        attackAble = true;
        tumbleAble = true;
        rotateAble = true;

        //애니메이터에 등록된 move들에게 현재 객체를 연결해줍니다. 
        InitMovement();

    }

    protected void InitMovement()
    {
        Movement[] movements = m_Animator.GetBehaviours<Movement>();
        for (int i = 0; i < movements.Length; i++)
        {
            movements[i].movementObject = this;
        }
    }

    protected virtual void Move()
    {
        if (!moveAble)
            return;
    }
    protected virtual void Rotate()
    {
        if (!rotateAble)
            return;
    }

    protected virtual void Jump()
    {
        if (!jumpAble)
            return;
    }

    protected virtual void Attack()
    {
        if (!attackAble)
            return;
    }
    protected virtual void Tumble()
    {
        if (!tumbleAble)
            return;
    }

    public virtual void OnDamage(int damage, bool down)
    {
        curHp -= damage;
        hpSlider.value = (float)curHp / maxHp;

        if (curHp <= 0)
        {
            OnDie();
        }
        else
        {
            if(down)
                m_Animator.SetTrigger("down");
            else
                m_Animator.SetTrigger("damage");

        }
    }

    public virtual void OnDie()
    {
        Collider[] colliders = GetComponents<Collider>();


        if (m_Rigidbody != null)
        {
            m_Rigidbody.isKinematic = true;
            m_Rigidbody.detectCollisions = false;
        }
        foreach (Collider collider in colliders)
        {
            collider.enabled = false;
        }

        m_Animator.SetTrigger("die");
    }
    public virtual void OnKnockback(Vector3 force)
    {
        m_Rigidbody.AddForce(force);
    }

    public void PlayClip(AudioClip clip)
    {
        m_AudioSource.clip = clip;
        m_AudioSource.Play();
    }

    public void Twinkle(float time)
    {
        StartCoroutine(TwinkleCo(time));
    }
    IEnumerator TwinkleCo(float time)
    {
        float startTime = Time.time;

        for (int i = 0; i < m_Renderers.Length; i++)
        {
            m_Renderers[i].material = twinkleMaterial;
        }
        yield return new WaitForSeconds(time);

        for (int i = 0; i < m_Renderers.Length; i++)
        {
            m_Renderers[i].material = originalMaterials[i];
        }
    }
}


