using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePieceMover : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;
    void Start()
    {

    }
    void Update()
    {
        
    }
    public void GetTex(int n)
    {
        SpriteRenderer s = GetComponent<SpriteRenderer>();
        s.sprite = sprites[n];
    }
}
