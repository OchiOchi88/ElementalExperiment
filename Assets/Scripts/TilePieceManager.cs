using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class TilePieceManager : MonoBehaviour
{
    [SerializeField] GameObject tilePrefab;
    [SerializeField] GameCtrler gc;
    List<TileSetter> ts = new List<TileSetter>();
    void Start()
    {

    }
    void Update()
    {

    }
    public void SetTilePiece(int i)
    {
        float x;
        float y;
        if (i <= 8)
        {
            x = 14.0f;
            y = i;
        }
        else
        {
            x = 16.5f;
            y = i - 9;
        }
        GameObject clone = Instantiate(tilePrefab, new Vector3(x, 8.0f - (y * 2.0f), 0), Quaternion.identity);
        TileSetter mover = clone.GetComponent<TileSetter>();
        while (ts.Count <= i)
        {
            ts.Add(null);
        }
        ts[i] = clone.GetComponent<TileSetter>();
        mover.GetTex(i);
    }
    public void GetSR()
    {
        foreach(TileSetter ants in ts)
        {
            Debug.Log(ants.GetComponent<SpriteRenderer>().sprite);
            ants.GetSR();
        }
    }
    public void CancelChoose()
    {
        foreach (TileSetter ants in ts)
        {
            ants.CancelChoose();
        }
    }
}
