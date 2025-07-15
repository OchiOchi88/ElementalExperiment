using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraManager : MonoBehaviour
{
    [SerializeField] GameObject cam;
    [SerializeField] TileTab tt;
    [SerializeField] SelectingTab st;
    [SerializeField] HiddenButton[] hb;
    [SerializeField] Button zoomIn;
    [SerializeField] Button zoomOut;
    int xMove = 0;
    int yMove = 0;
    bool isZoom = false;
    int moveDir = 0;
    private void Start()
    {
        zoomOut.interactable = false;
    }
    private void Update()
    {
        CameraMove();
    }
    public void ZoomIn()
    {
        if (isZoom == false)
        {
            cam.GetComponent<Camera>().orthographicSize -= 7;
            tt.ZoomIn();
            st.ZoomIn();
            isZoom = true;
            zoomIn.interactable = false;
            zoomOut.interactable = true;
            foreach (HiddenButton ahb in hb)
            {
                ahb.SetTrue();
            }
        }
    }
    public void ZoomOut()
    {
        if (isZoom == true)
        {
            cam.GetComponent<Camera>().orthographicSize += 7;
            tt.ZoomOut();
            st.ZoomOut();
            xMove = 0;
            yMove = 0;
            cam.transform.position = new Vector3(0, 0, -10);
            isZoom = false;
            zoomIn.interactable = true;
            zoomOut.interactable = false;
            foreach (HiddenButton ahb in hb)
            {
                ahb.SetFalse();
            }
        }
    }
    private void CameraMove()
    {
        switch (moveDir)
        {
            case 0:
                break;
            case 1:
                if (xMove <= 300)
                {
                    float move = cam.transform.position.x + 0.025f;
                    cam.transform.position = new Vector3(move, cam.transform.position.y, cam.transform.position.z);
                    xMove++;
                }
                break;
            case 2:
                if (xMove >= -300)
                {
                    float move = cam.transform.position.x - 0.025f;
                    cam.transform.position = new Vector3(move, cam.transform.position.y, cam.transform.position.z);
                    xMove--;
                }
                break;
            case 3:
                if (yMove <= 300)
                {
                    float move = cam.transform.position.y + 0.025f;
                    cam.transform.position = new Vector3(cam.transform.position.x, move, cam.transform.position.z);
                    yMove++;
                }
                break;
            case 4:
                if (yMove >= -300)
                {
                    float move = cam.transform.position.y - 0.025f;
                    cam.transform.position = new Vector3(cam.transform.position.x, move, cam.transform.position.z);
                    yMove--;
                }
                break;
        }
    }
    public void Right()
    {
        moveDir = 1;
    }
    public void Left()
    {
        moveDir = 2;
    }
    public void Up()
    {
        moveDir = 3;
    }
    public void Down()
    {
        moveDir = 4;
    }
    public void Stop()
    {
        moveDir = 0;
    }
}
