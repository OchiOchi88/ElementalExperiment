using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameCtrler : MonoBehaviour
{
    [SerializeField] int stage;
    [SerializeField] TileManager tileManager;
    [SerializeField] TileManager cloneTileManager;
    [SerializeField] ElementManager elementManager;
    [SerializeField] GoalElementManager goalElementManager;
    [SerializeField] TilePieceManager tpm;
    [SerializeField] TilePieceMover selectshower;
    [SerializeField] ResultManager clearPanel;
    [SerializeField] LastStageText lst;
    List<ElementMover> eMover = new List<ElementMover>();
    GameCtrler gc;
    int goalCount;
    int[] tileinfo = { 5, 5, 5, 6 };
    int[][] stageinfo = new int [][]
    {new int[]
        { -1,
            -1,
            -1,
            -1,
            -1,
            -1,
            -1,
            0,0,0,0,0,1,99,1,3,-1,
            0,0,0,0,0,0,0,0,1,-1,
            0,0,0,0,0,0,0,0,1,-1,
            0,0,0,0,0,0,0,0,1,-1,
            0,0,0,0,0,0,0,0,1,-1,
            0,0,0,0,0,0,0,0,99,1,1,1,1,-1,
            -1,
            -1,
            -1,
            -1,
            -1,
            -1,
            -1,
            -1},
        new int[]
        { -1,
            -1,
            -1,
            -1,
            -1,
            0,0,0,0,3,1,1,1,1,1,1,1,1,1,1,-1,
            0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,1,-1,
            0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,1,-1,
            0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,1,-1,
            0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,1,-1,
            0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,1,-1,
            0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,1,-1,
            0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,1,-1,
            0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,1,-1,
            0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,1,-1,
            0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,1,-1,
            0,0,0,0,2,1,1,1,1,1,1,1,1,1,1,5,-1,
            -1,
            -1,
            -1,
            -1
        },
        new int[]
        {
            -1,
            -1,
            -1,
            -1,
            -1,
            -1,
            -1,
            0,0,0,1,1,1,1,1,1,-1,
            -1,
            -1,
            0,0,0,0,0,1,1,1,1,1,1,-1,
            -1,
            -1,
            0,0,0,0,0,0,0,1,1,1,1,1,1,-1,
            -1,
            -1,
            -1,
            -1,
            -1,
            -1,
            -1
        },
        new int[]
        {
            -1,
            -1,
            -1,
            -1,
            -1,
            -1,
            -1,
            -1,
            -1,
            -1,
            0,0,0,0,0,0,0,1,99,99,99,99,1,-1,
            -1,
            -1,
            -1,
            -1,
            -1,
            -1,
            -1,
            -1,
            -1,
            -1
        }
    };

    int[] elementCount = { 2, 1, 3, 1 };
    int[,] elementSpos = { { -3, -5, 1 },{-3,-3,1 }, {-5,4,3}, { -3, -7, 1 }, { 0, -5, 1 }, { 3, -3, 1 },{ 0,-3,1} };
    int[,] elementGpos = { { 2, 2 },{2,0 },{-4 ,5}, { -3, -2 }, { 0, 0 }, { 3, 2 },{ 0, 2 } };
    int eTileCount = 0;
    void Start()
    {
        gc = GetComponent<GameCtrler>();
        int ind = 0;
        for (int x = -10; x < 11; x++)
        {
            for (int y = -10; y < 11; y++)
            {
                if (stageinfo[stage][ind] == -1)
                {
                    y = 11;
                    ind++;
                    continue;
                }
                else if (stageinfo[stage][ind] == 0)
                {
                    ind++;
                    continue;
                }
                else if (stageinfo[stage][ind] == 99)
                {
                    eTileCount++;
                    tileManager.SetTile(x, y, stageinfo[stage][ind], eTileCount);
                    ind++;
                }
                else
                {
                    tileManager.SetTile(x, y, stageinfo[stage][ind] + 1, eTileCount);
                    ind++;

                }
            }
        }
        for (int i = 0; i < tileinfo[stage]; i++)
        {
            tpm.SetTilePiece(i);
        }
        selectshower.SetTile();
        ElementSet();
    }
    public void SetStart()
    {
        tileManager.AllDelete();
        gc = GetComponent<GameCtrler>();
        int ind = 0;
        for (int x = -10; x < 11; x++)
        {
            for (int y = -10; y < 11; y++)
            {
                if (stageinfo[stage][ind] == -1)
                {
                    y = 11;
                    ind++;
                    continue;
                }
                else if (stageinfo[stage][ind] == 0)
                {
                    ind++;
                    continue;
                }
                else if (stageinfo[stage][ind] == 99)
                {
                    eTileCount++;
                    tileManager.SetTile(x, y, stageinfo[stage][ind],eTileCount);
                    ind++;
                }
                else
                {
                    tileManager.SetTile(x, y, stageinfo[stage][ind] + 1,eTileCount);
                    ind++;

                }
            }
        }
        for(int i = 0; i < tileinfo[stage]; i++)
        {
            tpm.SetTilePiece(i);
        }
        selectshower.ResetTile();
        ElementSet();
    }
    private void ElementSet()
    {
        elementManager.Restart();
        goalElementManager.Restart();
        goalCount = 0;
        int ec = 0;
        for(int j = 0; j < stage; j++) 
        {
            for (int i = 0; i < elementCount[j]; i++)
            {
                ec++;
            }
        }
        for (int i = 0; i < elementCount[stage]; i++)
        {
            goalElementManager.SetElement(elementGpos[ec, 0], elementGpos[ec, 1], i);
            elementManager.SetElement(elementSpos[ec, 0], elementSpos[ec, 1], i, elementSpos[ec, 2], gc);
            ec++;
        }
    }
    public void GetElementsComp(ElementMover em)
    {
        eMover.Add(em);
    }
    public void StartMove()
    {
        Debug.Log("ボタンが押された");
        foreach (ElementMover move in eMover)
        {
            move.StratMove();
        }
    }
    public int GetStage()
    {
        return stage;
    }
    public void Restart()
    {
        elementManager.Restart();
        goalElementManager.Restart();
        ElementSet();
    }
    public void Goal()
    {
        goalCount++;
        if(goalCount >= elementCount[stage])
        {
            clearPanel.Clear();
            if(stage == stageinfo.Length - 1)
            {
                lst.LastStageClear();
            }
        }
    }
    public bool NextStage()
    {
        if (stage < elementCount.Length -1)
        {
            stage++;
            return true;
        }
        else
        {
            return false;
        }
    }
}