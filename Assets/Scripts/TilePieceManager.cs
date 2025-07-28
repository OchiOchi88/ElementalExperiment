using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class TilePieceManager : MonoBehaviour
{
    [SerializeField] GameObject tilePrefab;
    [SerializeField] GameCtrler gc;
    List<TileSetter> ts = new List<TileSetter>();
    TileTab parent;
    void Start()
    {

    }
    void Update()
    {

    }
    public void SetTilePiece(int i ,int pos)
    {
        float x;
        float y;
        if (pos <= 8)
        {
            x = 14.0f;
            y = pos;
        }
        else
        {
            x = 16.5f;
            y = pos - 9;
        }
        parent = FindObjectOfType<TileTab>();
        GameObject clone = Instantiate(tilePrefab, new Vector3(x, 8.0f - (y * 2.0f), 0), Quaternion.identity,parent.transform);
        TileSetter mover = clone.GetComponent<TileSetter>();
        while (ts.Count <= pos)
        {
            ts.Add(null);
        }
        ts[pos] = clone.GetComponent<TileSetter>();
        mover.GetTex(i);
    }
    public void GetSR()
    {
        foreach (TileSetter ants in ts)
        {
            if (ants)
            {
                ants.GetSR();
            }
        }
    }
    public void CancelChoose()
    {
        foreach (TileSetter ants in ts)
        {
            if (ants)
            {
                ants.CancelChoose();
            }
        }
    }
}
