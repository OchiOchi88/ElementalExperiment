using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultManager : MonoBehaviour
{

    void Start()
    {
        gameObject.SetActive(false);
    }
    public void Clear()
    {
        gameObject.SetActive(true);
    }
    public void Retry()
    {
        GameCtrler gc = FindObjectOfType<GameCtrler>();
        gc.SetStart();
        gameObject.SetActive(false);
    }
    public void NextStage()
    {
        GameCtrler gc = FindObjectOfType<GameCtrler>();
        bool isexist = gc.NextStage();
        if (isexist)
        {
            gc.SetStart();
            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("このステージが最後のステージです！");
        }
    }
}