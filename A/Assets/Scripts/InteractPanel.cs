using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InteractPanel : MonoBehaviour
{
    public Text interactKeyText;

    [SerializeField]
    Player player;
    [SerializeField]
    GameObject interactPanel;
    [SerializeField]
    Image interactTouchIcon;
    [SerializeField]
    Text interactPanelText;

    List<IInteract> interactAbleList;

    void Start()
    {
#if UNITY_STANDALONE || UNITY_WEBPLAYER
        interactKeyText.text = SettingInfo.instance.interact.ToString();

        Destroy(interactTouchIcon.gameObject);
#elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
        Destroy(interactKeyText.gameObject);
#endif
    }



    void Update()
    {
        interactAbleList = player.onStayInteractList.FindAll(interactAbleList => interactAbleList.IsInteractAble());

        if (interactAbleList.Count != 0)
        {
            interactPanel.transform.position = Camera.main.WorldToScreenPoint(interactAbleList[interactAbleList.Count-1].GetPosition());
            interactPanelText.text = interactAbleList[interactAbleList.Count - 1].GetInteractText();
            interactPanel.SetActive(true);
        }
        else
        {
            interactPanel.SetActive(false);
        }
    }
}
