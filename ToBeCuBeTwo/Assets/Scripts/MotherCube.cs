using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherCube : MonoBehaviour
{
    //전체 코어
    public Core[] coreArray;
    //큐브를 가진 코어
    private List<Core> fullCoreArray = new List<Core>();

    public float rotSpeed;
    private Vector3 remainRot;

    private float curTime;
    public float lostTime { get; set; }

    public AudioSource audioSource;
    public AudioSource clapAudioSource;
    public ParticleSystem clapParticle;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.gameState == GameState.gaming)
        {
            curTime += Time.deltaTime;

            if (curTime >= lostTime)
            {
                LostRandomSonCube();

                curTime = 0f;
            }
        }
    }



    void FixedUpdate()
    {
        if (remainRot != Vector3.zero)
        {
            transform.Rotate(remainRot.normalized * rotSpeed, Space.World);
            remainRot -= remainRot.normalized * rotSpeed;
        }
    }


    public void fullSonCube()
    {
        foreach (Core core in coreArray)
        {
            core.GetSonCube();
        }
    }

    public void AddFullCoreArray(Core core)
    {
        fullCoreArray.Add(core);
    }

    void LostRandomSonCube()
    {
        int rnd = Random.Range(0, fullCoreArray.Count);
        fullCoreArray[rnd].LostSonCube();
        fullCoreArray.RemoveAt(rnd);
        audioSource.pitch = Random.Range(0.5f, 1.5f);
        audioSource.Play();

        if(fullCoreArray.Count == 0)
        {
            GameManager.instance.GameOver();
        }
    }

    public void Rotate(Vector3 rotAngle)
    {
        if(remainRot == Vector3.zero)
            remainRot += rotAngle;
    }


    public void Clap()
    {
        clapParticle.Play();
        clapAudioSource.Play();
    }

}
