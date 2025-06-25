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

        SpriteRenderer sr = clone.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.sprite= tileSprite[info-1];
        }
    }
}
