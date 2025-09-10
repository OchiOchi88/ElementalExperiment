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

        //Debug.Log("クリア画面表示完了判定");
        int nowLvl = NetworkManager.LoadUserLvl();
        Debug.Log("今のところのステージ進捗:" + nowLvl);
        Debug.Log("クリアしたステージ:" + GameCtrler.stage);
        if (nowLvl == GameCtrler.stage - 1)
        {
            int myAchieve = NetworkManager.LoadUserAchievement();
            string myName = NetworkManager.LoadUserName();
            nowLvl++;
            Debug.Log("自分の名前：" + myName);
            // ユーザーデータを更新して画面も更新
            StartCoroutine(NetworkManager.Instance.UpdateUser(
                myName,       // 名前
                nowLvl,              // レベル
　　　　　      result =>
            {     // 登録終了後の処理
                if (result == true)
                {

                    Debug.Log("クリアステージ情報更新が正常に終了しました。");
                    StartCoroutine(NetworkManager.Instance.LoadStageCount(
                        result => {                          //登録終了後の処理
                        if (result == true)
                        {
                            Debug.Log("ステージ数読み込み成功");
                            bool isLast = gc.IsLast();
                            if (isLast)
                            {
                                LastStageText lst = FindObjectOfType<LastStageText>();
                                lst.LastStageClear();
                            }
                        }
                        else
                        {
                            Debug.Log("ステージ数読み込み失敗");
                        }
                    }));
                    
                }
                else
                {
                    Debug.Log("クリアステージ情報更新が正常に終了しませんでした。");
                }
            }));
        }
        else
        {
            StartCoroutine(NetworkManager.Instance.LoadStageCount(
                result => {                          //登録終了後の処理
                if (result == true)
                {
                    Debug.Log("ステージ数読み込み成功");
                    bool isLast = gc.IsLast();
                    if (isLast)
                    {
                        LastStageText lst = FindObjectOfType<LastStageText>();
                        lst.LastStageClear();
                    }
                }
                else
                {
                    Debug.Log("ステージ数読み込み失敗");
                }
            }));
        }
    }
    public void Retry()
    {
        stage = GameCtrler.stage;
        StageSelector.startStage = GameCtrler.stage;
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