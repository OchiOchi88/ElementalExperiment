using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class TilePieceManager : MonoBehaviour
{
    [SerializeField] GameObject tilePrefab;
    [SerializeField] GameCtrler gc;
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
        TilePieceMover mover = clone.GetComponent<TilePieceMover>();
        mover.GetTex(i);
    }
}
