using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TitleController : MonoBehaviour
{
    [SerializeField] GameObject[] buttons;
    Color col = new Color(0, 0, 0);
    public void ButtonShow()
    {
        foreach(GameObject button in buttons)
        {
            button.SetActive(true);
        }
    }
    public void MainStart()
    {
        Initiate.Fade("StageSelectScene", col,1.5f);
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
    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
