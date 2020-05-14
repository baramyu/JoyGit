using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PopupManager : MonoBehaviour
{
    public static PopupManager instance;


    [SerializeField]
    RectTransform tooltip;
    [SerializeField]
    GameObject tooltipBG;
    [SerializeField]
    Text tooltipTitle;
    [SerializeField]
    Text tooltipInfo;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {

    }



    public void ShowTooltip(string title, string info, Vector2 pos)
    {
        tooltipTitle.text = title;
        tooltipInfo.text = info;
        tooltip.position = pos;
        tooltipBG.SetActive(true);
        tooltip.gameObject.SetActive(true);
    }
}
