using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.U2D.Animation;

public class TilePieceMover : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;
    [SerializeField] TileSetter ts;
    [SerializeField] GameObject go;
    Sprite sr;
    bool isTap = false;
    void OnMouseDown()
    {
        go = Instantiate(go, transform.position, Quaternion.identity);
        sr = transform.GetComponent<SpriteRenderer>().sprite;
        ts.SetSprite(sr);
        ts.Down();
    }
    // ’Ç‰Á
    void OnMouseUp()
    {
        ts.Up();
    }
    void Start()
    {

    }
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            isTap = false;
        }

    }
    public void GetTex(int n)
    {
        SpriteRenderer s = GetComponent<SpriteRenderer>();
        s.sprite = sprites[n];
    }
}
