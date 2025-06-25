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
    GameObject[] children;
    int i = 0;
    void Start()
    {

    }

    void Update()
    {
    }

    public void SetTile(int x,int y,int info)
    {
        Vector2 spawnPos = new Vector2(x, y);

        GameObject clone = Instantiate(prefab, spawnPos, Quaternion.identity);
        if (info == 1)
        {
            children[i] = clone;
            i++;
        }
        SpriteRenderer sr = clone.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.sprite= tileSprite[info-1];
        }
    }
    public Vector3 GetPos(int i)
    {
        return children[i].transform.position;
    }
}
