using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    AudioSource m_AudioSource;
    ParticleSystem[] m_ParticleSystems;

    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
        m_ParticleSystems = GetComponentsInChildren<ParticleSystem>();

        m_AudioSource.pitch = Random.Range(0.9f, 1.1f);
        m_AudioSource.Play();
        for(int i = 0; i < m_ParticleSystems.Length; i++)
        {
            m_ParticleSystems[i].Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (m_AudioSource.isPlaying)
            return;
        for(int i = 0; i < m_ParticleSystems.Length; i ++)
        {
            if (m_ParticleSystems[i].isPlaying)
                return;
        }

        Destroy(gameObject);
    }
}
