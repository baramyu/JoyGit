using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour
{
    //조이스틱
    public GameObject virtualJoystickBG;
    public GameObject virtualJoystick;
    public float maxMagnitude;
    public static Vector3 input { get; set; }
    public static Vector3 input2d { get; set; }
    
    private Vector3 downPos;
    private Vector3 dragPos;
    private Vector3 dir;
    private float magnitude;




    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_STANDALONE || UNITY_WEBPLAYER 
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            downPos = Input.mousePosition;



            //화면에서 왼쪽 반 부분을 눌렀을때만 동작
            if(downPos.x < Screen.width/2f)
            {
                virtualJoystickBG.transform.position = downPos;
                virtualJoystickBG.SetActive(true);

            }
            
            
        }
        if(Input.GetMouseButton(0))
        {
            if (!virtualJoystickBG.activeSelf)
            {
                return;
            }

            dragPos = Input.mousePosition;
            dir = Vector3.Normalize(dragPos - downPos);
            magnitude = Vector3.Magnitude(dragPos - downPos);
            if(magnitude > maxMagnitude)
            {
                downPos += dir * (magnitude - maxMagnitude);
                virtualJoystickBG.transform.position = downPos;
            }
            input2d = dir *  Mathf.Min(magnitude / maxMagnitude, 1f);
            input = new Vector3(input2d.x, 0f, input2d.y);

            virtualJoystick.transform.position = downPos + input2d * maxMagnitude;
        }
        if (Input.GetMouseButtonUp(0))
        {
            input = Vector3.zero;

            virtualJoystickBG.SetActive(false);
        }
//#elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    return;
                }
                downPos = touch.position;
                //화면 좌측
                if (downPos.x < Screen.width / 2f)
                {
                    virtualJoystickBG.transform.position = downPos;
                    virtualJoystickBG.SetActive(true);

                }
            }
            if(touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                if (!virtualJoystickBG.activeSelf)
                {
                    return;
                }

                dragPos = touch.position;
                dir = Vector3.Normalize(dragPos - downPos);
                magnitude = Vector3.Magnitude(dragPos - downPos);
                if (magnitude > maxMagnitude)
                {
                    downPos += dir * (magnitude - maxMagnitude);
                    virtualJoystickBG.transform.position = downPos;
                }
                input2d = dir * Mathf.Min(magnitude / maxMagnitude, 1f);
                input = new Vector3(input2d.x, 0f, input2d.y);
                virtualJoystick.transform.position = downPos + input2d * maxMagnitude;

            }
            if (touch.phase == TouchPhase.Ended)
            {
                input = Vector3.zero;
                virtualJoystickBG.SetActive(false);

            }
        }
        
#endif
    }
}
