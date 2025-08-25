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
                //���[�U�[�f�[�^���ۑ�����ĂȂ��ꍇ�͓o�^
                StartCoroutine(NetworkManager.Instance.RegistUser(
                    Guid.NewGuid().ToString(),           //���O
                    0,
                    0,
                    0,
                result => {                          //�o�^�I����̏���
                    if (result == true)
                    {
                        SceneManager.LoadScene("StageSelectScene");
                    }
                    else
                    { 
                        Debug.Log("���[�U�[�o�^������ɏI�����܂���ł����B");
                        isTouched = false;
                    }
                }));
            }
        }
    }
}
