using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DataManager : MonoBehaviour
{
    public Item test;

    public static DataManager instance;
    List<Item> inventory;

    private void Awake()
    {
        instance = this;

        inventory = new List<Item>();
        inventory.Add(test);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Item GetItem(int index)
    {
        Item item;

        item = inventory.Find(i => i.index == index);

        return item;
    }
}
