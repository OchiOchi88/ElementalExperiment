using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TitleController : MonoBehaviour
{
    [SerializeField] GameObject[] buttons; 
    public void ButtonShow()
    {
        foreach(GameObject button in buttons)
        {
            button.SetActive(true);
        }
    }
    public void MainStart()
    {
        
    }
    public void CreateStart()
    {

    }
    public void SreachStart()
    {

    }
    public void ConfigStart()
    {

    }
}
