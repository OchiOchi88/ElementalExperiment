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
    List<ElementMover> eMover = new List<ElementMover>();
    GameCtrler gc;
    int count = 0;
    int[] tileinfo = { 5, 5, 5, 6 };
    int[,] stageinfo = {
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
            -1}};
    int[] elementCount = { 2, 1, 1, 2 };
    int[,] elementSpos = { { -3, -5, 1 },{-3,-3,1 } };
    int[,] elementGpos = { { 2, 2 },{2,0 } };
    int eTileCount = 0;
    void Start()
    {
        gc = GetComponent<GameCtrler>();
        int ind = 0;
        for (int x = -10; x < 11; x++)
        {
            for (int y = -10; y < 11; y++)
            {
                if (stageinfo[stage, ind] == -1)
                {
                    y = 11;
                    ind++;
                    continue;
                }
                else if (stageinfo[stage, ind] == 0)
                {
                    ind++;
                    continue;
                }
                else if (stageinfo[stage, ind] == 99)
                {
                    tileManager.SetTile(x, y, stageinfo[stage,ind]);
                    ind++;
                    eTileCount++;
                }
                else
                {
                    tileManager.SetTile(x, y, stageinfo[stage, ind] + 1);
                    ind++;

                }
            }
        }
        for (int i = 0; i < elementCount[stage]; i++)
        {
            goalElementManager.SetElement(elementGpos[i, 0], elementGpos[i, 1], i);
            elementManager.SetElement(elementSpos[i, 0], elementSpos[i, 1], i, elementSpos[i, 2],gc);
        }
        for(int i = 0; i < tileinfo[stage]; i++)
        {
            tpm.SetTilePiece(i);
        }
    }
    public void GetElementsComp(ElementMover em)
    {
        eMover.Add(em);
    }
    public void StartMove()
    {
        Debug.Log("ƒ{ƒ^ƒ“‚ª‰Ÿ‚³‚ê‚½");
        foreach (ElementMover move in eMover)
        {
            move.StratMove();
        }
    }
    public int GetStage()
    {
        return stage;
    }
    public float GetDis( Vector3 tp)
    {
        float[] dis = { };
        float fDis = 999.9f;
        for (int i = 0; i < eTileCount; i++)
        {
            Vector3 targetPos = cloneTileManager.GetPos(i);
            dis[i] = Vector3.Distance(tp, targetPos);
        }
        fDis = dis[0];
        for(int i = 0; i < dis.Length - 1; i++)
        {
            for(int j = i + 1; j < dis.Length; j++)
            {
                if (dis[i] >= dis[j])
                {
                    fDis = dis[j];
                }
            }
        }
        return fDis;
    }
}