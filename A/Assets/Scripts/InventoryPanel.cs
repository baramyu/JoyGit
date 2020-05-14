using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanel : MonoBehaviour
{
    [SerializeField]
    ItemSlot slotPrefab;
    [SerializeField]
    float slotWidth;
    [SerializeField]
    float slotGap;
    [SerializeField]
    int slotCol;
    [SerializeField]
    int slotRow;
    [SerializeField]
    RectTransform inPanel;

    // Start is called before the first frame update
    void Start()
    {
        float panelWidth = slotWidth * slotCol + slotGap * (slotCol-1);
        float panelHeight = slotWidth * slotRow + slotGap * (slotRow - 1);

        inPanel.sizeDelta = new Vector2(panelWidth, panelHeight);
        for(int i = 0; i < slotCol; i++)
        {
            for(int j = 0; j < slotRow; j++)
            {
                ItemSlot slot = Instantiate(slotPrefab, inPanel.transform);
                slot.SetRect((slotWidth + slotGap) * i, -(slotWidth + slotGap) * j, slotWidth, slotWidth);
                slot.SetItem(i*slotRow + j);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
