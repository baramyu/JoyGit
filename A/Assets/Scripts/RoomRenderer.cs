using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomRenderer : MonoBehaviour
{

    PlayerController playerController;
    public bool showed { get; set; }

    Renderer[] topRenderers;
    Renderer[] bottomRenderers;
    Renderer[] side1Renderers;
    Renderer[] side2Renderers;
    Renderer[] side3Renderers;
    Renderer[] side4Renderers;
    Renderer[] etcRenderers;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        topRenderers = transform.Find("Top").GetComponentsInChildren<Renderer>();
        bottomRenderers = transform.Find("Bottom").GetComponentsInChildren<Renderer>();
        side1Renderers = transform.Find("Side1").GetComponentsInChildren<Renderer>();
        side2Renderers = transform.Find("Side2").GetComponentsInChildren<Renderer>();
        side3Renderers = transform.Find("Side3").GetComponentsInChildren<Renderer>();
        side4Renderers = transform.Find("Side4").GetComponentsInChildren<Renderer>();
        etcRenderers = transform.Find("Etc").GetComponentsInChildren<Renderer>();

        HideRoom();
    }


    public void ShowRoom()
    {

        foreach (Renderer renderer in topRenderers)
        {
            renderer.enabled = true;
        }
        foreach (Renderer renderer in bottomRenderers)
        {
            renderer.enabled = true;
        }
        foreach (Renderer renderer in side1Renderers)
        {
            renderer.enabled = true;
        }
        foreach (Renderer renderer in side2Renderers)
        {
            renderer.enabled = true;
        }
        foreach (Renderer renderer in side3Renderers)
        {
            renderer.enabled = true;
        }
        foreach (Renderer renderer in side4Renderers)
        {
            renderer.enabled = true;
        }
        foreach (Renderer renderer in etcRenderers)
        {
            renderer.enabled = true;
        }

    }

    public void ShowQuadRoom()
    {
        foreach (Renderer renderer in bottomRenderers)
        {
            renderer.enabled = true;
        }
        foreach (Renderer renderer in side1Renderers)
        {
            renderer.enabled = true;
        }
        foreach (Renderer renderer in side2Renderers)
        {
            renderer.enabled = true;
        }
        foreach (Renderer renderer in etcRenderers)
        {
            renderer.enabled = true;
        }


    }

    public void HideRoom()
    {
        foreach (Renderer renderer in topRenderers)
        {
            renderer.enabled = false;
        }
        foreach (Renderer renderer in bottomRenderers)
        {
            renderer.enabled = false;
        }
        foreach (Renderer renderer in side1Renderers)
        {
            renderer.enabled = false;
        }
        foreach (Renderer renderer in side2Renderers)
        {
            renderer.enabled = false;
        }
        foreach (Renderer renderer in side3Renderers)
        {
            renderer.enabled = false;
        }
        foreach (Renderer renderer in side4Renderers)
        {
            renderer.enabled = false;
        }
        foreach (Renderer renderer in etcRenderers)
        {
            renderer.enabled = false;
        }

    }

    void ReDraw()
    {
        if (playerController.curRooms.Count == 0)
            return;

        else if (playerController.curRooms.Count < 2)
            playerController.curRooms[0].ShowQuadRoom();

        else
        {
            int i = 0;

            while(i < playerController.curRooms.Count - 1)
            {
                playerController.curRooms[i].ShowQuadRoom();

                i++;
            }
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(!playerController.curRooms.Contains(this))
            {
                playerController.curRooms.Add(this);
                ReDraw();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        playerController.curRooms.Remove(this);
        HideRoom();
    }
}
