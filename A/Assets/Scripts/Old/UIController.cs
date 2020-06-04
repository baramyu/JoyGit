using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    Player player;

    [SerializeField]
    Image attackBuuton;
    [SerializeField]
    Image jumpBuuton;
    [SerializeField]
    Image tumbleBuuton;


    // Update is called once per frame
    void Update()
    {
        if(player.attackAble)
        {
            attackBuuton.color = Color.white;
        }
        else
        {
            attackBuuton.color = Color.gray;
        }

        if (player.jumpAble && player.curJumpNum > 0)
        {
            jumpBuuton.color = Color.white;
        }
        else
        {
            jumpBuuton.color = Color.gray;
        }

        if (player.tumbleAble)
        {
            tumbleBuuton.color = Color.white;
        }
        else
        {
            tumbleBuuton.color = Color.gray;
        }
    }
}
