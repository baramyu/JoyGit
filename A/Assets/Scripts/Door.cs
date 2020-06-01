using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteract
{


    public bool isLock;
    public bool isOpen;
    public float openTime;

    [SerializeField]
    ParticleSystem blueLight;
    [SerializeField]
    ParticleSystem redLight;
    [SerializeField]
    AudioSource m_audioSource;
    [SerializeField]
    AudioClip moveSound;
    [SerializeField]
    AudioClip stopSound;

    void Start()
    {
        if (isOpen)
        {
            StartCoroutine(OpenCor(openTime));
        }
        else
        {
            if (isLock)
                Lock();
            else
                UnLock();
        }
    }

    


    public void Interact()
    {
        if (!isOpen && !isLock)
        {
            StartCoroutine(OpenCor(openTime));
        }
    }

    public bool IsInteractAble()
    {
        if (isOpen)
            return false;
        else
            return true;
    }

    public string GetInteractText()
    {
        if (isLock)
            return "잠김";
        else
            return "열기";
    }



    public void Lock()
    {
        isLock = true;
        blueLight.gameObject.SetActive(false);
        redLight.gameObject.SetActive(true);
    }
    public void UnLock()
    {
        isLock = false;
        redLight.gameObject.SetActive(false);
        blueLight.gameObject.SetActive(true);
    }



    IEnumerator OpenCor(float openTime)
    {
        float startYPos = transform.position.y;
        isOpen = true;

        blueLight.gameObject.SetActive(false);
        redLight.gameObject.SetActive(false);

        m_audioSource.loop = true;
        m_audioSource.clip = moveSound;
        m_audioSource.Play();

        while (transform.position.y > startYPos - 3f)
        {
            transform.position -= transform.up * startYPos * Time.fixedDeltaTime / openTime;

            yield return null;
        }

        m_audioSource.loop = false;
        m_audioSource.clip = stopSound;
        m_audioSource.Play();

    }


    public void Close(float closeTime, bool locked)
    {
        StartCoroutine(CloseCor(closeTime, locked));
    }

    IEnumerator CloseCor(float closeTime, bool locked)
    {
        float startYPos = transform.position.y;

        m_audioSource.loop = true;
        m_audioSource.clip = moveSound;
        m_audioSource.Play();

        while (transform.position.y < startYPos + 3f)
        {
            transform.position -= transform.up * startYPos * Time.fixedDeltaTime / closeTime;

            yield return null;
        }

        m_audioSource.loop = false;
        m_audioSource.clip = stopSound;
        m_audioSource.Play();

        if(locked)
        {
            Lock();
        }
        else
        {
            UnLock();
        }
        isOpen = false;
    }
    public Vector3 GetPosition()
    {
        return transform.position;
    }


}
