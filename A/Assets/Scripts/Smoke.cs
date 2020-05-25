using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke : MonoBehaviour
{
    public float delay;

    float lastTime;
    ParticleSystem m_particleSystem;
    AudioSource m_audioSource;

    // Start is called before the first frame update
    void Start()
    {
        lastTime = Time.time;
        m_particleSystem = GetComponent<ParticleSystem>();
        m_audioSource = GetComponent<AudioSource>();
    }


    private void FixedUpdate()
    {
        if(Time.time > lastTime + delay)
        {
            m_particleSystem.Play();
            m_audioSource.pitch = Random.Range(1.0f, 1.5f);
            m_audioSource.Play();
            lastTime = Time.time;
        }
    }
}
