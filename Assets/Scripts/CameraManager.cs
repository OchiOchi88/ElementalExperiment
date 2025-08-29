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
    [SerializeField] TilePieceMover tpm;
    float xMove = 0;
    float yMove = 0;
    float zoomValue = 0;
//    int moveDir = 0;
    int zooming = 0;
    const float speed = 0.075f;
    FixedJoystick joystick;
    private void Start()
    {
        joystick = GameObject.Find("Fixed Joystick").GetComponent<FixedJoystick>();
        zoomOut.interactable = false;
        //tpm = FindObjectOfType<TilePieceMover>();
    }
    private void FixedUpdate()
    {
        CameraMove();
        ZoomIn();
        ZoomOut();

    }
    public void ZoomIn()
    {
        if (zooming == 1 && zoomValue < 100)
        {
            cam.GetComponent<Camera>().orthographicSize -= 0.07f;
            tt.ZoomIn();
            st.ZoomIn();
            tpm.ZoomIn();
            zoomValue += 1.0f;
            if (zoomValue == 100)
            {
                zoomIn.interactable = false;
            }
            zoomOut.interactable = true;
        }
    }
    public void ZoomOut()
    {
        if (zooming == -1 && zoomValue > 0)
        {
            cam.GetComponent<Camera>().orthographicSize += 0.07f;
            tt.ZoomOut();
            st.ZoomOut();
            tpm.ZoomOut();
            zoomValue -= 1.0f;
            if (xMove >= zoomValue)
            {
                xMove--;
                float move = cam.transform.position.x - speed;
                cam.transform.position = new Vector3(move, cam.transform.position.y, cam.transform.position.z);
            }
            if (yMove >= zoomValue)
            {
                yMove--;
                float move = cam.transform.position.y - speed;
                cam.transform.position = new Vector3(cam.transform.position.x, move, cam.transform.position.z);
            }
            if (xMove <= -zoomValue)
            {
                xMove++;
                float move = cam.transform.position.x + speed;
                cam.transform.position = new Vector3(move, cam.transform.position.y, cam.transform.position.z);
            }
            if (yMove <= -zoomValue)
            {
                yMove++;
                float move = cam.transform.position.y + speed;
                cam.transform.position = new Vector3(cam.transform.position.x, move, cam.transform.position.z);
            }
            //cam.transform.position = new Vector3(0, 0, -10);
            zoomIn.interactable = true;
            if (zoomValue == 0)
            {
                zoomOut.interactable = false;
            }
        }
    }
    private void CameraMove()
    {
        Vector2 move = new Vector2(
    joystick.Horizontal * speed,
    joystick.Vertical * speed
    );
        if (xMove >= zoomValue && joystick.Horizontal > 0)
        {
            move.x = 0;
            xMove = zoomValue;
        }
        if (xMove <= -zoomValue && joystick.Horizontal < 0)
        {
            move.x = 0;
            xMove = -zoomValue;
        }
        if (yMove >= zoomValue && joystick.Vertical > 0)
        {
            move.y = 0;
            yMove = zoomValue;
        }
        if (yMove <= -zoomValue && joystick.Vertical < 0)
        {
            move.y = 0;
            yMove = -zoomValue;
        }

        cam.transform.position += new Vector3(move.x, move.y, 0);
        xMove += joystick.Horizontal;
        yMove += joystick.Vertical;
    }

    //public void Right()
    //{
    //    moveDir = 1;
    //}
    //public void Left()
    //{
    //    moveDir = 2;
    //}
    //public void Up()
    //{
    //    moveDir = 3;
    //}
    //public void Down()
    //{
    //    moveDir = 4;
    //}
    public void Stop()
    {
        //moveDir = 0;
        zooming = 0;
    }
    public void In()
    {
        zooming = 1;
    }
    public void Out()
    {
        zooming = -1;
    }
}
