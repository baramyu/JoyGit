using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Part
{
    hat,
    upper,
    lower,
    weapon
}

public class Item_Equipment : Item
{
    private Part part;
    private bool wearing;

}
