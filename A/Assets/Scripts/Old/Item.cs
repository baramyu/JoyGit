using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type
{
    etc,
    used,
    equipment
}

public class Item : MonoBehaviour
{
    public int code;
    public Type type;
    public string title;
    public string info;
    public Sprite image;//test중
    public int index;
}