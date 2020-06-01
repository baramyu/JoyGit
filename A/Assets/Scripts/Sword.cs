using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour, IInteract
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
        return "뽑기";
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void Interact()
    {
        //칼 세팅
        player.GetSword();
        isInteractAble = false;
        Destroy(gameObject);
    }

    public bool IsInteractAble()
    {
        return isInteractAble;
    }
}
