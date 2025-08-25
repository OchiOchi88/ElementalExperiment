using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndWarning : MonoBehaviour
{
    bool isTouched = false;
    void Update()
    {

    }
    public void StartGame()
    {
        Initiate.Fade("TitleScene", new Color(0, 0, 0), 1.0f);
        if (!isTouched)
        {
            isTouched = true;
            bool isSuccess = NetworkManager.Instance.LoadUserData();
            if (isSuccess)
            {
                SceneManager.LoadScene("StageSelectScene");
            }
            else
            {
                //ユーザーデータが保存されてない場合は登録
                StartCoroutine(NetworkManager.Instance.RegistUser(
                    Guid.NewGuid().ToString(),           //名前
                    0,
                    0,
                    0,
                result => {                          //登録終了後の処理
                    if (result == true)
                    {
                        SceneManager.LoadScene("StageSelectScene");
                    }
                    else
                    { 
                        Debug.Log("ユーザー登録が正常に終了しませんでした。");
                        isTouched = false;
                    }
                }));
            }
        }
    }
}
