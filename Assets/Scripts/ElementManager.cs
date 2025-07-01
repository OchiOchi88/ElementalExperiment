using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ElementManager : MonoBehaviour
{
    [SerializeField] Sprite[] ElementSprite;
    public GameObject prefab;
    public GameObject currentPrefab;
    public GameCtrler gameCtrler;
    private Vector3 targetPosition;
    Rigidbody2D rb;
    SpriteRenderer sr;
    int moveDir;
    bool ismove;
    GameObject currentInstance;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }
    public void SetElement(int x, int y, int color, int dir , GameCtrler controller)
    {
        Vector3 spawnPos = new Vector3(x, y, 0);

        GameObject clone = Instantiate(currentPrefab, spawnPos, Quaternion.identity);

        Debug.Log("生成開始：" + dir);

        ElementMover mover = clone.GetComponent<ElementMover>();
        //gm =GetComponent<GameCtrler>();
        gameCtrler = controller;
        gameCtrler.GetElementsComp(mover);
        if (mover != null)
        {
            mover.SetManager(this);
            mover.StartMove(dir);
            mover.SetColor(color);
        }
        else
        {
            Debug.LogError("ElementMover が見つかりません！");
        }

        if (ElementSprite.Length > color && ElementSprite[color] != null)
        {
            SpriteRenderer sr = clone.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.sprite = ElementSprite[color];
            }
        }
        else
        {
            Debug.LogWarning($"ElementSprite[{color}] が未設定です！");
        }
    }
    void Update()
    {
    }
    public int getDir()
    {
        Debug.Log("送信：" + moveDir);
        return moveDir;
    }
    public SpriteRenderer GetColor()
    {
        return sr;
    }
}
