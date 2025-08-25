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
                Debug.Log("ユーザー登録済みです。セーブデータをロードします。");
            }
            else
            {
                //ユーザーデータが保存されてない場合は登録
                StartCoroutine(NetworkManager.Instance.RegistUser(
                    Guid.NewGuid().ToString(),           //名前
                    0,      //  クリアステージ数
                    0,      //  実績
                result => {                          //登録終了後の処理
                    if (result == true)
                    {
                        Debug.Log("ユーザー登録が正常に終了しました。");
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
