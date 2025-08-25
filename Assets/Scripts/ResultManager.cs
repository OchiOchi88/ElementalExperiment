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
        Debug.Log("�N���A��ʕ\����������");
        int nowLvl = NetworkManager.LoadUserLvl();
            // ���[�U�[�f�[�^���X�V���ĉ�ʂ��X�V
            StartCoroutine(NetworkManager.Instance.UpdateUser(
                NetworkManager.nameData,       // ���O
                nowLvl + 1,              // ���x��
                0,             //  �o���l
                0,              //  ����
�@�@�@�@�@      result => {     // �o�^�I����̏���
                if (result == true)
                {
                   Debug.Log("���[�U�[���X�V������ɏI�����܂����B");
               }
                else
                {
                    Debug.Log("���[�U�[���X�V������ɏI�����܂���ł����B");

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
            Debug.Log("���̃X�e�[�W���Ō�̃X�e�[�W�ł��I");
        }
    }
    public void BacktoSelect()
    {
        stage = 0;
        Initiate.Fade("StageSelectScene", col, 1.0f);
    }
}