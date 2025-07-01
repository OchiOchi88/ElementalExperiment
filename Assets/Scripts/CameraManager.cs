using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] GameObject cam;
    bool isfar = true;
    public void Zoom()
    {
        if(isfar== true)
        {
            cam.GetComponent<Transform>().position = new Vector3(0,0,-10);
        }
        else
        {

        }
    }
    public void Right()
    {

    }
    public void Left()
    {

    }
    public void Up()
    {
        
    }
    public void Down()
    {
        
    }
}
