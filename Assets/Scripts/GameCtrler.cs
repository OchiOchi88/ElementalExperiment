using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using Unity.Properties;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class GameCtrler : MonoBehaviour
{
    static public int clearStage = 0;
    static public int stage;
    UnityEngine.Color col = new UnityEngine.Color(0, 0, 0);
    [SerializeField] TileManager tileManager;
    [SerializeField] TileManager cloneTileManager;
    [SerializeField] ElementManager elementManager;
    [SerializeField] GoalElementManager goalElementManager;
    [SerializeField] TilePieceManager tilePieceManager;
    [SerializeField] TilePieceMover selectShower;
    [SerializeField] ResultManager clearPanel;
    [SerializeField] LastStageText lastStageText;
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject pauseButton;
    List<ElementMover> eMover = new List<ElementMover>();
    GameCtrler gc;
    int goalCount;
    static public int[] tileX;
    static public int[] tileY;
    static public int[] tileType;

    static public int[] startElementX;
    static public int[] startElementY;
    static public int[] startElementType;
    static public int[] goalElementX;
    static public int[] goalElementY;
    static public int[] goalElementType;

    static public int[] palette;
    static private int paletteLen = 0;
    static public int stageCount = 0;
    static private int startElemCount = 0;
    static private int goalElemCount = 0;

    int[][] tileInfo = new int [][] { new int[] { },
        new int[]{ 1,1,1,1,1 },
        new int[]{ 1,1,1,1,1 },
        new int[]{ 1,1,1,1,1 },
        new int[]{ 1,1,1,1,1,0,0,0,0,0,0,1,1,1 },
        new int[]{ 1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,1},
        new int[]{ 1,1,1,1,1,0,0,0,0,0,0,1,1,1,0,0,0,0,0,0,1} };
    int[][] stageInfo = new int [][]
    {new int[]
    {
        -1
    },
        new int[]
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
            0,0,0,0,3,1,1,1,99,1,99,1,1,1,1,-1,
            0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,1,-1,
            0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,1,-1,
            0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,1,-1,
            0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,1,-1,
            0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,1,-1,
            0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,1,-1,
            0,0,0,0,99,0,0,0,0,0,0,0,0,0,0,1,-1,
            0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,1,-1,
            0,0,0,0,99,0,0,0,0,0,0,0,0,0,0,99,-1,
            0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,99,-1,
            0,0,0,0,99,1,1,1,1,1,1,1,1,99,99,5,-1,
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
            0,0,0,1,1,1,1,99,1,-1,
            -1,
            -1,
            0,0,0,0,0,1,1,1,99,1,1,-1,
            -1,
            -1,
            0,0,0,0,0,0,0,1,1,99,1,1,1,-1,
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
            0,0,0,0,0,0,0,0,0,0,1,-1,
            0,0,0,0,0,0,0,0,0,0,1,-1,
            0,0,0,0,0,0,0,0,0,0,1,-1,
            0,0,0,0,0,0,0,0,0,0,1,-1,
            0,0,0,0,0,0,0,0,0,0,99,-1,
            0,0,0,0,0,0,0,0,0,0,99,-1,
            0,0,0,0,1,1,1,1,99,99,99,99,99,1,1,1,1,-1,
            0,0,0,0,0,0,0,0,0,0,99,-1,
            0,0,0,0,0,0,0,0,0,0,99,-1,
            0,0,0,0,0,0,0,0,0,0,1,-1,
            0,0,0,0,0,0,0,0,0,0,1,-1,
            0,0,0,0,0,0,0,0,0,0,1,-1,
            0,0,0,0,0,0,0,0,0,0,1,-1,
            0,0,0,1,1,1,1,1,1,1,4,-1,
            -1,
            -1,
            -1
        },
        new int[]
        {
            -1,
            -1,
            0,0,0,0,0,1,1,1,1,1,3,1,1,1,1,1,-1,
            0,0,0,0,0,0,0,0,0,0,1,-1,
            0,0,0,0,0,0,0,0,0,0,1,-1,
            0,0,0,0,0,0,0,0,0,0,1,-1,
            0,0,0,0,0,1,1,1,1,1,3,1,1,1,1,1,-1,
            0,0,0,0,0,0,0,0,0,0,1,-1,
            0,0,0,0,0,0,0,0,0,0,1,-1,
            0,0,0,0,0,0,13,1,1,1,99,-1,
            0,0,0,0,0,0,1,0,0,0,99,-1,
            0,0,0,0,0,0,1,0,0,0,9,-1,
            0,0,0,0,0,0,1,0,0,0,1,-1,
            0,0,0,0,0,0,13,1,1,1,3,-1,
            0,0,0,0,0,0,0,0,0,0,1,-1,
            0,0,0,0,0,0,0,0,0,0,1,-1,
            0,0,0,0,0,0,0,0,0,0,1,-1,
            0,0,0,0,0,0,0,0,0,0,1,-1,
            0,0,0,0,0,0,0,0,0,0,1,-1,
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
            0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,-1,
            0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,-1,
            0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,-1,
            0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,-1,
            0,0,0,0,0,0,1,1,1,1,12,1,1,21,1,1,3,-1,
            0,0,0,0,0,0,0,0,0,0,1,-1,
            0,0,0,0,0,0,0,0,0,0,1,-1,
            0,0,0,0,0,0,0,0,0,0,21,-1,
            0,0,0,0,0,0,0,0,0,0,1,-1,
            0,0,0,0,0,0,0,0,0,0,7,-1,
            0,0,0,0,0,0,0,0,0,0,1,-1,
            0,0,0,0,0,0,0,0,0,0,3,-1,
            -1,
            -1,
            -1,
            -1
        }
    };

    int[] elementCount = {0, 2, 1, 3, 4 ,4,1};
    int[,] elementSpos = { 
        { -3, -5, 1 },{-3,-3,1 }, 
        {-5,4,3}, 
        { -3, -7, 1 }, { 0, -5, 1 }, { 3, -3, 1 }, 
        { 0, -6, 1 }, { -5, 0, 2 }, { 0, 4, 3 }, { 3, 0, 4 } ,
        { -8, 5, 3 }, { -8, -3, 1 }, { -4, -5, 1 }, { -4, 3, 3 },
        { -1,-4,1}
    };
    int[,] elementGpos = { 
        { 2, 2 },{2,0 },
        {-4 ,5}, 
        { -3, -2 }, { 0, 0 }, { 3, 2 },
        { 7, -7 }, { 7, -6 }, { 7, -5 }, { 7, -4 },
        { 5, 0 }, { 6, 0 }, { 7, 0 }, { 8, 0 },
        { -5,6}
    };
    int eTileCount = 0;
    void Start()
    {

        goalCount = 0;

        //  ステージ選択のシーンからステージのレベルを受け取る
        if(ResultManager.stage != 0)
        {
            stage = ResultManager.stage;

        }
        else if(StageSelector.startStage != 0)
        {
            stage = StageSelector.startStage;

        }
        else
        {
            stage = 1;
        }
            //Debug.Log(StageSelector.startStage);

        //  受け取ったステージのレベルを元にDBからステージを受け取る
            gc = GetComponent<GameCtrler>();
        StartCoroutine(NetworkManager.Instance.GetTileData(stage,
                result => {                          //登録終了後の処理
                    if (result == true)
                    {
                        Debug.Log("ステージ読み込み成功");
                        startElemCount = 0;
                        goalElemCount = 0;
                        GenerateStage();
                    }
                    else
                    {
                        Debug.Log("ステージ読み込み失敗");
                    }
                }));
        //(tileX, tileY, tileType) = NetworkManager.Instance.SendTileData();
        //(eleX, eleY, eleType) = NetworkManager.Instance.GetElementData(stage);
        //(PaletteX, PaletteY, PaletteType) = NetworkManager.Instance.GetPlaetteData(stage);
        //
        //  APIでステージのデータ(x座標、y座標、タイルのタイプ)などを取得し、各変数に代入
        //





        //int ind = 0;
        //for (int x = -10; x < 11; x++)
        //{
        //    for (int y = -10; y < 11; y++)
        //    {
        //        if (stageInfo[stage][ind] == -1)
        //        {
        //            y = 11;
        //            ind++;
        //            continue;
        //        }
        //        else if (stageInfo[stage][ind] == 0)
        //        {
        //            ind++;
        //            continue;
        //        }
        //        else if (stageInfo[stage][ind] == 99)
        //        {
        //            eTileCount++;
        //            tileManager.SetTile(x, y, stageInfo[stage][ind], eTileCount);
        //            ind++;
        //        }
        //        else
        //        {
        //            tileManager.SetTile(x, y, stageInfo[stage][ind] + 1, eTileCount);
        //            ind++;

        //        }
        //    }
        //}


    }
    private void GenerateStage()
    {
        Debug.Log(tileType);

        //  ステージ生成
        for (int tile = 0; tile < tileType.Length; tile++)
        {
            //Debug.Log(tileType.Length);
            //Debug.Log(tileType[tile]);
            if (tileType[tile] != 99)
            {
                eTileCount++;
                tileType[tile]++;
            }
            tileManager.SetTile(tileX[tile], tileY[tile], tileType[tile], eTileCount);
        }

        //  パレットの生成
        int tilePlaced = 0;
        foreach (int tile in palette)
        {
            tilePieceManager.SetTilePiece(tile, tilePlaced);
                tilePlaced++;
        }
        //for (int i = 0; i < tileinfo[stage]; i++)
        //{
        //    tpm.SetTilePiece(i);
        //}

        selectShower.SetTile(); //  選択中のタイルを表示するUIのセット
        ElementSet();           //  元素の生成
    }
    public void SetStart()
    {
        goalCount = 0;
        tileManager.AllDelete();
        gc = GetComponent<GameCtrler>();


        foreach (int tiles in tileType)
        {
            eTileCount++;
            tileManager.SetTile(tileX[tiles], tileY[tiles], tileType[tiles], eTileCount);
        }


        //int ind = 0;
        //for (int x = -10; x < 11; x++)
        //{
        //    for (int y = -10; y < 11; y++)
        //    {
        //        if (stageinfo[stage][ind] == -1)
        //        {
        //            y = 11;
        //            ind++;
        //            continue;
        //        }
        //        else if (stageinfo[stage][ind] == 0)
        //        {
        //            ind++;
        //            continue;
        //        }
        //        else if (stageinfo[stage][ind] == 99)
        //        {
        //            eTileCount++;
        //            tileManager.SetTile(x, y, stageinfo[stage][ind],eTileCount);
        //            ind++;
        //        }
        //        else
        //        {
        //            tileManager.SetTile(x, y, stageinfo[stage][ind] + 1,eTileCount);
        //            ind++;

        //        }
        //    }
        //}
        //  パレットの生成
        int tilePlaced = 0;
        foreach (int tile in palette)
        {
            tilePieceManager.SetTilePiece(tile, tilePlaced);
            tilePlaced++;
        }
        //for (int i = 0; i < tileinfo[stage]; i++)
        //{
        //    tpm.SetTilePiece(i);
        //}
        selectShower.ResetTile();
        ElementSet();
    }
    private void ElementSet()
    {
        //  元素が動いているかもなので止める
        elementManager.Restart();
        goalElementManager.Restart();
        goalCount = 0;

        for(int i = 0; i < startElementType.Length; i++)
        {
            elementManager.SetElement(startElementX[i], startElementY[i],i, startElementType[i], gc);
            goalElementManager.SetElement(goalElementX[i], goalElementY[i], i);
        }

        //  元素の生成        //int ec = 0;
        //for(int j = 0; j < stage; j++) 
        //{
        //    for (int i = 0; i < elementCount[j]; i++)
        //    {
        //        ec++;
        //    }
        //}
        //for (int i = 0; i < elementCount[stage]; i++)
        //{
        //    goalElementManager.SetElement(elementGpos[ec, 0], elementGpos[ec, 1], i);
        //    elementManager.SetElement(elementSpos[ec, 0], elementSpos[ec, 1], i, elementSpos[ec, 2], gc);
        //    ec++;
        //}

    }
    public void GetElementsComp(ElementMover em)
    {
        eMover.Add(em);
    }
    public void StartMove()
    {
        //Debug.Log("ボタンが押された");
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
        tileManager.Restart();
    }
    public void Goal()
    {
        goalCount++;
        Debug.Log(goalCount);
        if (goalCount >= goalElementType.Length)
        {
            Debug.Log("クリア判定");
            pauseButton.SetActive(false);
            clearPanel.gameObject.SetActive(true);
            clearPanel.Clear();
        }
    }
    public bool NextStage()
    {
        if (stage < stageCount)
        {
            stage++;
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool IsLast()
    {
        if (stage >= stageCount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void Pause()
    {
        pausePanel.SetActive(true);
        elementManager.Pause();
    }
    public void Replay()
    {
        pausePanel.SetActive(false);
        elementManager.Replay();
    }
    public void RestartStage()
    {
        Initiate.Fade("PuzzleScene", col, 2.0f);
    }
    public void BacktoSelect()
    {
        StageSelector.startStage = 0;
        ResultManager.stage = 0;
        Initiate.Fade("StageSelectScene", col, 1.5f);
    }
    public void BacktoTitle()
    {
        StageSelector.startStage = 0;
        ResultManager.stage = 0;
        Initiate.Fade("TitleScene", col, 1.5f);
    }
    public void StageClear()
    {
        if(clearStage <= stage)
        {
            clearStage = stage;
        }

    }
    static public void InitTile(int count)
    {
        tileX = new int[count];
        tileY = new int[count];
        tileType = new int[count];
    }
    static public void InitElement(int start,int goal)
    {
        startElemCount = 0;
        goalElemCount = 0;
        goalElementX = new int[goal];
        goalElementY = new int[goal];
        goalElementType = new int[goal];
        startElementX = new int[start];
        startElementY = new int[start];
        startElementType = new int[start];
        Debug.Log("Init完了:"+start + "  " + goal);
    }
    static public void InitPalette(int count)
    {
        paletteLen = 0;
        palette = new int[count];
    }
    static public void GetTileData(int x, int y, int type,int i) 
    {
        Debug.Log(x);
        tileX[i] = x;
        tileY[i] = y;
        tileType[i] = type;
        //Debug.Log("タイル情報スタンバイ");
    }
    static public void GetElementData(int x, int y, int type)
    {

        Debug.Log("受信"+x+y+type);
        if (type== 0)
        {
            goalElementX[goalElemCount] = x;
            goalElementY[goalElemCount] = y;
            goalElementType[goalElemCount] = type;
            goalElemCount++;
        }
        else
        {
            startElementX[startElemCount] = x;
            startElementY[startElemCount] = y;
            startElementType[startElemCount] = type;
            startElemCount++;
        }
        //Debug.Log("タイル情報スタンバイ");
    }
    static public void GetPaletteData(int type)
    {
        //Debug.Log(x);
        //tileX[i] = x;
        //tileY[i] = y;
        palette[paletteLen] = type -1;
        paletteLen++;
        //Debug.Log("タイル情報スタンバイ");
    }
    static public void GetStageCount(int count)
    {
        stageCount = count;
        Debug.Log("stageCount:" + stageCount);
    }
}