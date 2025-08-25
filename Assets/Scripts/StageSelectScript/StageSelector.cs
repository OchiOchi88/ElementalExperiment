using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System;
using static RegistUserRepuest;
using UnityEngine.Networking;

public class StageSelector : MonoBehaviour
{
    [SerializeField] GameObject stageButton;
    [SerializeField] Button nextBtutton;
    [SerializeField] Button backBtutton;
    List<GameObject> children = new List<GameObject>();
    public int maxStage;
    int page;
    int nowPage = 0;
    bool isChanged;
    static public int startStage;
    Color col = new Color(0, 0, 0);
    RectTransform rtf;

    private int lvl;    //  自分のステージデータ
    private string apiToken;    //  APIトークン

    //  プロパティ
    public string APIToken
    {
        get
        {
            return this.apiToken;
        }
    }
    public int Lvl
    {
        get
        {
            return this.lvl;
        }
    }
    void Start()
    {
        lvl = NetworkManager.LoadUserLvl();
        maxStage = lvl + 1;
        Debug.Log(maxStage);

        rtf = GetComponent<RectTransform>();
        rtf.anchoredPosition = new Vector3(0, 0, 0);
        isChanged = false;
        int fraction = maxStage % 10;
        if (fraction == 0)
        {
            page = maxStage / 10 - 1;
        }
        else
        {
            page = maxStage / 10;
        }
        SettingStage();
        backBtutton.interactable = false;
        if(maxStage <= 10)
        {
            nextBtutton.interactable = false;
        }
    }
    private void SettingStage()
    {
        if (isChanged)
        {
            foreach (GameObject child in children)
            {
                child.GetComponent<StageSetter>().Delete();
            }
            children = new List<GameObject>();
            isChanged = false;
        }
        Vector3 pos;
        int fraction;
        if (nowPage >= page)
        {
            fraction = maxStage % 10;
            if(fraction == 0)
            {
                fraction = 10;
            }
        }
        else
        {
            fraction = 10;
        }
            for (int i = 1; i <= fraction; i++)
            {
                if (i > 5)
                {
                    pos = new Vector3((i * 120) - 960, -65, 0);
                }
                else
                {
                    pos = new Vector3((i * 120) - 360, 50, 0);
                }
                GameObject clone = Instantiate(stageButton, pos, Quaternion.identity, transform);
                rtf = clone.GetComponent<RectTransform>();    
                clone.GetComponent<StageSetter>().SetPos(pos);
                clone.GetComponent<StageSetter>().SetStage(i + (nowPage * 10));
            rtf.anchoredPosition = pos;
            while (children.Count <= i -1)
            {
                children.Add(null);
            }
            children[i-1] = clone;
        }
    }

    public void NextPage()
    {
        isChanged = true;
        nowPage++;
        SettingStage();
        if (nowPage >= page)
        {
            nextBtutton.interactable = false;
        }
        backBtutton.interactable = true;
    }
    public void BackPage()
    {
        isChanged = true;
        nowPage--;
        SettingStage();
        if (nowPage <= 0)
        {
            backBtutton.interactable = false;
        }
        nextBtutton.interactable = true;
    }
    public void StartStage(int setStage)
    {
        startStage = setStage;
        Initiate.Fade("PuzzleScene", col, 1.5f);
    }
    public void BackToTitle()
    {
        Initiate.Fade("TitleScene", col, 1.5f);
    }

}
