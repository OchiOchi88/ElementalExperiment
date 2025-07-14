using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultManager : MonoBehaviour
{
    static public int stage = 0;
    Color col = new Color(0, 0, 0);
    void Start()
    {
        enabled = false;
    }
    public void Clear()
    {
        enabled = true;
        GameCtrler gc = FindObjectOfType<GameCtrler>();
        bool isLast = gc.IsLast();
        if (isLast)
        {
            LastStageText lst = FindObjectOfType<LastStageText>();
            lst.LastStageClear();
        }
        Debug.Log("クリア画面表示完了判定");
    }
    public void Retry()
    {
        stage = GameCtrler.stage;
        GameCtrler gc = FindObjectOfType<GameCtrler>();
        //gc.SetStart();
        Initiate.Fade("PuzzleScene", col, 2.0f);

    }
    public void NextStage()
    {
        GameCtrler gc = FindObjectOfType<GameCtrler>();
        bool isexist = gc.NextStage();
        if (isexist)
        {
            stage = GameCtrler.stage;
            //gc.SetStart();
            Initiate.Fade("PuzzleScene", col, 2.0f);

        }
        else
        {
            Debug.Log("このステージが最後のステージです！");
        }
    }
    public void BacktoSelect()
    {
        stage = 0;
        Initiate.Fade("StageSelectScene", col, 1.0f);
    }
}