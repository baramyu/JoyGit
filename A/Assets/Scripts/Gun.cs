using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour, IInteract
{
    [SerializeField]
    Player player;

    bool isInteractAble;

    void Start()
    {
        isInteractAble = true;
    }

    public string GetInteractText()
    {
        return "'총' 획득";
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void Interact()
    {
        //총 세팅
        player.GetGun();
        isInteractAble = false;
        Destroy(gameObject);
    }

    public bool IsInteractAble()
    {
        return isInteractAble;
    }
}
