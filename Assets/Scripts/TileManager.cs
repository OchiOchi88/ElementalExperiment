using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TileManager : MonoBehaviour
{
    [SerializeField] Sprite[] tileSprite;
    [SerializeField] GameObject prefab;
    List<TileCollider> tc = new List<TileCollider>();
    List<TileCollider> allTile = new List<TileCollider>();
    int i = 0;
    void Start()
    {
    }

    void Update()
    {
    }

    public void SetTile(int x, int y, int info, int eTileNumber)
    {
        Vector2 spawnPos = new Vector2(x, y);

        GameObject clone = Instantiate(prefab, spawnPos, Quaternion.identity);
        if (info == 99)
        {
            //Debug.Log(eTileNumber + "番送信");
            clone.GetComponent<TileCollider>().SetVoid(eTileNumber);
            while (tc.Count <= eTileNumber)
            {
                tc.Add(null);
            }
            tc[eTileNumber] = clone.GetComponent<TileCollider>();
        }
        SpriteRenderer sr = clone.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            if (info == 99)
            {
                sr.sprite = tileSprite[0];
            }
            else
            {
                sr.sprite = tileSprite[info - 1];
            }
        }
        while(allTile.Count <= i)
        {
            allTile.Add(null);
        }
        allTile[i] = clone.GetComponent<TileCollider>();
        i++;
    }
    public void StandBy()
    {
        //Debug.Log("マネージャースタンバイ");
        foreach (TileCollider atc in tc)
        {
            if (atc)
            {
                Debug.Log(atc);
                atc.StandBy();
            }
        }
    }
    public void GetsRenderer( Sprite sr)
    {
        //Debug.Log("sr(BeforeHolder):" + sr);
        Sprite srHolder = sr;
        foreach (TileCollider atc in tc)
        {
            if (atc)
            { 
                bool isTouched = atc.GetStand();
                if (isTouched == true)
                {
                    atc.GetsRenderer(srHolder);
                }
            }
        }
    }
    public void AllDelete()
    {
        foreach(TileCollider aTile in allTile)
        {
            if (aTile != null)
            {
                aTile.Delete();
            }
        }
        i = 0;
    }
}
