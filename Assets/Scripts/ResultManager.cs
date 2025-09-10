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

        //Debug.Log("�N���A��ʕ\����������");
        int nowLvl = NetworkManager.LoadUserLvl();
        Debug.Log("���̂Ƃ���̃X�e�[�W�i��:" + nowLvl);
        Debug.Log("�N���A�����X�e�[�W:" + GameCtrler.stage);
        if (nowLvl == GameCtrler.stage - 1)
        {
            int myAchieve = NetworkManager.LoadUserAchievement();
            string myName = NetworkManager.LoadUserName();
            nowLvl++;
            Debug.Log("�����̖��O�F" + myName);
            // ���[�U�[�f�[�^���X�V���ĉ�ʂ��X�V
            StartCoroutine(NetworkManager.Instance.UpdateUser(
                myName,       // ���O
                nowLvl,              // ���x��
�@�@�@�@�@      result =>
            {     // �o�^�I����̏���
                if (result == true)
                {

                    Debug.Log("�N���A�X�e�[�W���X�V������ɏI�����܂����B");
                    StartCoroutine(NetworkManager.Instance.LoadStageCount(
                        result => {                          //�o�^�I����̏���
                        if (result == true)
                        {
                            Debug.Log("�X�e�[�W���ǂݍ��ݐ���");
                            bool isLast = gc.IsLast();
                            if (isLast)
                            {
                                LastStageText lst = FindObjectOfType<LastStageText>();
                                lst.LastStageClear();
                            }
                        }
                        else
                        {
                            Debug.Log("�X�e�[�W���ǂݍ��ݎ��s");
                        }
                    }));
                    
                }
                else
                {
                    Debug.Log("�N���A�X�e�[�W���X�V������ɏI�����܂���ł����B");
                }
            }));
        }
        else
        {
            StartCoroutine(NetworkManager.Instance.LoadStageCount(
                result => {                          //�o�^�I����̏���
                if (result == true)
                {
                    Debug.Log("�X�e�[�W���ǂݍ��ݐ���");
                    bool isLast = gc.IsLast();
                    if (isLast)
                    {
                        LastStageText lst = FindObjectOfType<LastStageText>();
                        lst.LastStageClear();
                    }
                }
                else
                {
                    Debug.Log("�X�e�[�W���ǂݍ��ݎ��s");
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
            Debug.Log("���̃X�e�[�W���Ō�̃X�e�[�W�ł��I");
        }
    }
    public void BacktoSelect()
    {
        stage = 0;
        Initiate.Fade("StageSelectScene", col, 1.0f);
    }
}