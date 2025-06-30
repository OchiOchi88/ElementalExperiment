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
    GameObject[] eChildren = {};
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
        if (info == 99)
        {
            clone.GetComponent<TileCollider>().SetVoid(i);
            i++;
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
    }
    public Vector3 GetPos(int i)
    {
        return eChildren[i].transform.position;
    }
}
