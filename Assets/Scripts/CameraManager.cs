using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] GameObject cam;
    [SerializeField] TileTab tt;
    [SerializeField] SelectingTab st;
    [SerializeField] HiddenButton[] hb;
    int xMove = 0;
    int yMove = 0;
    bool isZoom = false;
    public void ZoomIn()
    {
        if (isZoom == false)
        {
            cam.GetComponent<Camera>().orthographicSize -= 7;
            tt.ZoomIn();
            st.ZoomIn();
            isZoom = true;
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
            foreach(HiddenButton ahb in hb)
            {
                ahb.SetFalse();
            }
        }
    }
    public void Right()
    {
        if(xMove <= 1)
        {
            float move = cam.transform.position.x + 3.5f;
            cam.transform.position = new Vector3(move,cam.transform.position.y, cam.transform.position.z);
            xMove++;
        }
    }
    public void Left()
    {
        if (xMove >= -1)
        {
            float move = cam.transform.position.x - 3.5f;
            cam.transform.position = new Vector3(move, cam.transform.position.y, cam.transform.position.z);
            xMove--;
        }
    }
    public void Up()
    {
        if (yMove <= 1)
        {
            float move = cam.transform.position.y + 3.5f;
            cam.transform.position = new Vector3(cam.transform.position.x, move, cam.transform.position.z);
            yMove++;
        }
    }
    public void Down()
    {
        if (yMove >= -1)
        {
            float move = cam.transform.position.y - 3.5f;
            cam.transform.position = new Vector3(cam.transform.position.x, move, cam.transform.position.z);
            yMove--;
        }
    }
}
