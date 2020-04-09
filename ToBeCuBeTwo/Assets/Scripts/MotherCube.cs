using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherCube : MonoBehaviour
{
    //전체 코어
    public Core[] coreArray;
    //큐브를 가진 코어
    private List<Core> fullCoreArray = new List<Core>();
    

    // Start is called before the first frame update
    void Start()
    {
        foreach(Core core in coreArray)
        {
            core.GetSonCube();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            LostRandomSonCube();
    }

    public void AddFullCore(Core core)
    {
        fullCoreArray.Add(core);
    }

    void LostRandomSonCube()
    {
        int rnd = Random.Range(0, fullCoreArray.Count);
        fullCoreArray[rnd].LostSonCube();
        fullCoreArray.RemoveAt(rnd);
    }

}
