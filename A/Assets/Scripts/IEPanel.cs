using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IEPanel : MonoBehaviour
{
    public ItemSlot itemSlotPrefab;
    public int maxSlotNum;


    [SerializeField]
    RectTransform itemInPanel;
    float itemSlotWidth;
    int row;

    void Start()
    {
        itemSlotWidth = itemSlotPrefab.GetComponent<RectTransform>().rect.width;
        itemInPanel.sizeDelta -= new Vector2(itemInPanel.rect.width % itemSlotWidth, 0f);
        row = (int)(itemInPanel.rect.width / itemSlotWidth);
        //content.sizeDelta += new Vector2(0f, maxSlotNum / row * itemSlotWidth);

        int slotCnt = 0;
        while(slotCnt < maxSlotNum)
        {
            ItemSlot itemSlot = Instantiate(itemSlotPrefab, itemInPanel.transform);
            itemSlot.GetComponent<RectTransform>().localPosition = new Vector2(slotCnt%row*itemSlotWidth, -slotCnt/row*itemSlotWidth);
            //itemSlot.item = DataManager.instance.GetItem(slotCnt);
            slotCnt++;
        }

    }
}
