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
        gc.StageClear();
        bool isLast = gc.IsLast();
        if (isLast)
        {
            LastStageText lst = FindObjectOfType<LastStageText>();
            lst.LastStageClear();
        }
        Debug.Log("クリア画面表示完了判定");
        int nowLvl = NetworkManager.LoadUserLvl();
            // ユーザーデータを更新して画面も更新
            StartCoroutine(NetworkManager.Instance.UpdateUser(
                NetworkManager.nameData,       // 名前
                nowLvl + 1,              // レベル
                0,             //  経験値
                0,              //  所属
　　　　　      result => {     // 登録終了後の処理
                if (result == true)
                {
                   Debug.Log("ユーザー情報更新が正常に終了しました。");
               }
                else
                {
                    Debug.Log("ユーザー情報更新が正常に終了しませんでした。");

                }
            }));
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