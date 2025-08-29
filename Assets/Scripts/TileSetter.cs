using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSetter : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;
    bool isTouched = false;
    [SerializeField] GameCtrler gc;
    SpriteRenderer sRenderer;
    [SerializeField] TileCollider tc;
    [SerializeField] Sprite[] sprites;
    [SerializeField] TileManager tm;
    [SerializeField] TilePieceMover ts;
    Sprite mySprite;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void SetSprite(Sprite sr)
    {
        sRenderer = GetComponent<SpriteRenderer>();
        sRenderer.sprite = sr;
        mySprite = sr;
    }
    // Update is called once per frame
    void Update()
    {
    }
    //  空白タイルがタッチされたなら事前にパレットからタイルが選ばれていれば設置する処理
    public void OnMouseDown()
    {
        tm.StandBy();
        isTouched = true;
        ts.SetTile(transform.GetComponent<SpriteRenderer>().sprite);
    }
    public void GetSR()
    {
        if(isTouched == true)
        {
            tm.GetsRenderer(transform.GetComponent<SpriteRenderer>().sprite);
        }
    }
    //  パレットからほかのタイルが選択されたときに直前まで選んでいたタイルの設置スタンバイをキャンセルする処理
    public void CancelChoose()
    {
        if (isTouched == true)
        {
            Debug.Log(transform.GetComponent<SpriteRenderer>().sprite.name + "はスタンバイをキャンセルしました！");
            isTouched = false;
        }
    }
    public void GetTex(int n)
    {
        SpriteRenderer s = GetComponent<SpriteRenderer>();
        s.sprite = sprites[n];
    }
}
