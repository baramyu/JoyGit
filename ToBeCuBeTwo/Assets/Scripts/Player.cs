using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    public KeyCode shot0;
    public KeyCode shot1;
    public KeyCode shot2;
    public KeyCode shot3;
    public KeyCode shot4;
    public KeyCode shot5;
    public KeyCode shot6;
    public KeyCode shot7;
    public KeyCode shot8;
    public KeyCode rotRight;
    public KeyCode rotLeft;
    public KeyCode rotUp;
    public KeyCode rotDown;
    public MotherCube motherCube;

    public SonCube sonCubePrefab;
    public int sonCubePoolSize;
    private SonCube[] sonCubePool;
    private int poolIndex;
    public float shotPower;
    public Vector3[] spawnPoint;


    private Vector2 touchBeganPos;
    private Vector2 touchEndedPos;
    private Vector2 touchDif;
    private float swipeSensitivity;

    private Rect screen;
    private float blockDistance;

    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        sonCubePool = new SonCube[sonCubePoolSize];
        poolIndex = 0;
        for (int i = 0; i < sonCubePoolSize; i++)
        {
            sonCubePool[i] = Instantiate(sonCubePrefab);
            sonCubePool[i].gameObject.SetActive(false);
        }
        screen = GameManager.instance.screen;
        blockDistance = GameManager.instance.screen.width / 3f;
    }


    void MakeSonCube(Vector3 spawnPoint)
    {
        sonCubePool[poolIndex].gameObject.SetActive(true);
        sonCubePool[poolIndex].Shot(shotPower, spawnPoint);
        audioSource.pitch = Random.Range(0.5f, 1.5f);
        audioSource.Play();
        if (++poolIndex >= sonCubePoolSize)
        {
            poolIndex = 0;
        }

    }


    // Update is called once per frame
    void Update()
    {
#if UNITY_STANDALONE
        //keybord
        if(Input.GetKeyDown(shot0))
        {
            MakeSonCube(spawnPoint[0]);
        }
        if (Input.GetKeyDown(shot1))
        {
            MakeSonCube(spawnPoint[1]);
        }
        if (Input.GetKeyDown(shot2))
        {
            MakeSonCube(spawnPoint[2]);
        }
        if (Input.GetKeyDown(shot3))
        {
            MakeSonCube(spawnPoint[3]);
        }
        if (Input.GetKeyDown(shot4))
        {
            MakeSonCube(spawnPoint[4]);
        }
        if (Input.GetKeyDown(shot5))
        {
            MakeSonCube(spawnPoint[5]);
        }
        if (Input.GetKeyDown(shot6))
        {
            MakeSonCube(spawnPoint[6]);
        }
        if (Input.GetKeyDown(shot7))
        {
            MakeSonCube(spawnPoint[7]);
        }
        if (Input.GetKeyDown(shot8))
        {
            MakeSonCube(spawnPoint[8]);
        }
        
        if (Input.GetKeyDown(rotUp))
        {
            motherCube.Rotate(Vector3.right * -90f);
        }
        if (Input.GetKeyDown(rotDown))
        {
            motherCube.Rotate(Vector3.right * 90f);
        }
        if (Input.GetKeyDown(rotRight))
        {
            motherCube.Rotate(Vector3.up * -90f);
        }
        if (Input.GetKeyDown(rotLeft))
        {
            motherCube.Rotate(Vector3.up * 90f);
        }

        //mouse
        if (Input.GetMouseButtonDown(0))
        {
            touchBeganPos = Input.mousePosition;
        }
        if (!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonUp(0))
        {
            touchEndedPos = Input.mousePosition;
            touchDif = (touchEndedPos - touchBeganPos);

            //up
            if (touchDif.y > 0 && Mathf.Abs(touchDif.y) > swipeSensitivity && Mathf.Abs(touchDif.y) > Mathf.Abs(touchDif.x))
            {
                Debug.Log("up" + touchDif);
                motherCube.Rotate(Vector3.right * -90f);
            }
            //down
            else if (touchDif.y < 0 && Mathf.Abs(touchDif.y) > swipeSensitivity && Mathf.Abs(touchDif.y) > Mathf.Abs(touchDif.x))
            {
                Debug.Log("down" + touchDif);
                motherCube.Rotate(Vector3.right * 90f);
            }
            //right
            else if (touchDif.x > 0 && Mathf.Abs(touchDif.x) > swipeSensitivity && Mathf.Abs(touchDif.x) > Mathf.Abs(touchDif.y))
            {
                Debug.Log("right" + touchDif);
                motherCube.Rotate(Vector3.up * -90f);
            }
            //left
            else if (touchDif.x < 0 && Mathf.Abs(touchDif.x) > swipeSensitivity && Mathf.Abs(touchDif.x) > Mathf.Abs(touchDif.y))
            {
                Debug.Log("left" + touchDif);
                motherCube.Rotate(Vector3.up * 90f);
            }
            //touch
            else
            {
                int touchZone = JudgeTouchZone(touchEndedPos);
                if(touchZone != -1)
                    MakeSonCube(spawnPoint[touchZone]);
            }

        }

#elif UNITY_IOS || UNITY_ANDROID
      
        if (!EventSystem.current.IsPointerOverGameObject() && Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            

            if (touch.phase == TouchPhase.Began)
            {
                touchBeganPos = touch.position;
            }
            if (touch.phase == TouchPhase.Ended)
            {
                touchEndedPos = touch.position;
                touchDif = (touchEndedPos - touchBeganPos);

                //up
                if (touchDif.y > 0 && Mathf.Abs(touchDif.y) > swipeSensitivity && Mathf.Abs(touchDif.y) > Mathf.Abs(touchDif.x))
                {
                    Debug.Log("up" + touchDif);
                    motherCube.Rotate(Vector3.right * -90f);
                }
                //down
                else if (touchDif.y < 0 && Mathf.Abs(touchDif.y) > swipeSensitivity && Mathf.Abs(touchDif.y) > Mathf.Abs(touchDif.x))
                {
                    Debug.Log("down" + touchDif);
                    motherCube.Rotate(Vector3.right * 90f);
                }
                //right
                else if (touchDif.x > 0 && Mathf.Abs(touchDif.x) > swipeSensitivity && Mathf.Abs(touchDif.x) > Mathf.Abs(touchDif.y))
                {
                    Debug.Log("right" + touchDif);
                    motherCube.Rotate(Vector3.up * -90f);
                }
                //left
                else if (touchDif.x < 0 && Mathf.Abs(touchDif.x) > swipeSensitivity && Mathf.Abs(touchDif.x) > Mathf.Abs(touchDif.y))
                {
                    Debug.Log("left" + touchDif);
                    motherCube.Rotate(Vector3.up * 90f);
                }
                //touch
                else
                {
                    int touchZone = JudgeTouchZone(touchEndedPos);
                    if(touchZone != -1)
                        MakeSonCube(spawnPoint[touchZone]);
                }

            }
        }

          
#endif
    }
    


    private int JudgeTouchZone(Vector2 touchPos)
    {

        if (touchPos.y >= screen.y && touchPos.y < screen.y + blockDistance)
        {
            if (touchPos.x >= screen.x && touchPos.x < screen.x + blockDistance)
            {
                return 0;
            }
            else if (touchPos.x >= screen.x + blockDistance && touchPos.x < screen.x + blockDistance * 2f)
            {
                return 1;
            }
            else if (touchPos.x >= screen.x + blockDistance * 2f && touchPos.x < screen.x + blockDistance * 3f)
            {
                return 2;
            }
        }
        else if(touchPos.y >= screen.y + blockDistance && touchPos.y  < screen.y + blockDistance * 2f)
        {
            if (touchPos.x >= screen.x && touchPos.x < screen.x + blockDistance)
            {
                return 3;
            }
            else if (touchPos.x >= screen.x + blockDistance && touchPos.x < screen.x + blockDistance * 2f)
            {
                return 4;
            }
            else if (touchPos.x >= screen.x + blockDistance * 2f && touchPos.x < screen.x + blockDistance * 3f)
            {
                return 5;
            }
        }
        else if(touchPos.y >= screen.y + blockDistance * 2f && touchPos.y < screen.y + blockDistance * 3f)
        {
            if (touchPos.x >= screen.x && touchPos.x < screen.x + blockDistance)
            {
                return 6;
            }
            else if (touchPos.x >= screen.x + blockDistance && touchPos.x < screen.x + blockDistance * 2f)
            {
                return 7;
            }
            else if (touchPos.x >= screen.x + blockDistance * 2f && touchPos.x < screen.x + blockDistance * 3f)
            {
                return 8;
            }
        }

        return -1;
    }

    public void SetSwipeSensitivity(float swipeSensitivity)
    {
        this.swipeSensitivity = Screen.width * swipeSensitivity * 0.5f;
    }
}
