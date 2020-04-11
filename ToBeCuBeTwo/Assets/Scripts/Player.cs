using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public KeyCode shot1;
    public KeyCode shot2;
    public KeyCode shot3;
    public KeyCode shot4;
    public KeyCode shot5;
    public KeyCode shot6;
    public KeyCode shot7;
    public KeyCode shot8;
    public KeyCode shot9;
    public KeyCode rotRight;
    public KeyCode rotLeft;
    public KeyCode rotUp;
    public KeyCode rotDown;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_STANDALONE
        if(Input.GetKeyDown(shot1))
        {

        }
        if (Input.GetKeyDown(shot2))
        {

        }
        if (Input.GetKeyDown(shot3))
        {

        }
        if (Input.GetKeyDown(shot4))
        {

        }
        if (Input.GetKeyDown(shot5))
        {

        }
        if (Input.GetKeyDown(shot6))
        {

        }
        if (Input.GetKeyDown(shot7))
        {

        }
        if (Input.GetKeyDown(shot8))
        {

        }
        if (Input.GetKeyDown(shot9))
        {

        }
        if (Input.GetKeyDown(rotRight))
        {

        }
        if (Input.GetKeyDown(rotLeft))
        {

        }
        if (Input.GetKeyDown(rotUp))
        {

        }
        if (Input.GetKeyDown(rotDown))
        {

        }
        //#elif UNITY_IOS || UNITY_ANDROID
        Swipe();
#endif
    }


    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;

    public void Swipe()
    {
        if (Input.touches.Length > 0)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Began)
            {
                //save began touch 2d point
                firstPressPos = new Vector2(t.position.x, t.position.y);
            }
            if (t.phase == TouchPhase.Ended)
            {
                //save ended touch 2d point
                secondPressPos = new Vector2(t.position.x, t.position.y);

                //create vector from the two points
                currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

                //normalize the 2d vector
                currentSwipe.Normalize();

                //swipe upwards
                if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                {
                    Debug.Log("up swipe");
                }
                //swipe down
                if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                {
                    Debug.Log("down swipe");
                }
                //swipe left
                if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                {
                    Debug.Log("left swipe");
                }
                //swipe right
                if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                {
                    Debug.Log("right swipe");
                }
            }
        }
    }
}
