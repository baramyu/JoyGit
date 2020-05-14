using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    Item item;

    [SerializeField]
    RectTransform rectTransform;
    [SerializeField]
    Image itemImage;

    void Start()
    {
    }


    public void SetRect(float x, float y, float w, float h)
    {

        rectTransform.sizeDelta = new Vector2(w, h);
        rectTransform.localPosition = new Vector2(x, y);
    }
    public void SetItem(int index)
    {
        item = DataManager.instance.GetItem(index);

        if(item != null)
        {
            itemImage.sprite = item.image;
            itemImage.color = Color.white;
        }
    }
    /*
    public void ItemClick()
    {
        if (item == null)
            return;
        PopupManager.instance.ShowTooltip(item.title, item.info, Input.mousePosition);
    }

    float i = 0f;
    public void ItemPointerDown()
    {
        i += Time.deltaTime;

        Debug.Log(i);
    }






    public void ItemDrag()
    {
        if (item == null)
            return;

        itemImage.transform.position = Input.mousePosition;
    }
    public void ItemEndDrag()
    {
        if (item == null)
            return;

        itemImage.transform.position = transform.position;
    }*/
}
