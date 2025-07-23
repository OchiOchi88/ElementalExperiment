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
    public void OnMouseDown()
    {
        tm.StandBy();
        isTouched = true;
        if(isTouched == true)
        {
            //Debug.Log(transform.GetComponent<SpriteRenderer>().sprite.name + " : " + transform.gameObject.name + "���X�^���o�C�����I");
        }
        ts.SetTile(transform.GetComponent<SpriteRenderer>().sprite);
    }
    public void GetSR()
    {
        //Debug.Log(transform.gameObject.name);
        if(isTouched == true)
        {
            //Debug.Log("mySprite:" + transform.GetComponent<SpriteRenderer>().sprite);
            tm.GetsRenderer(transform.GetComponent<SpriteRenderer>().sprite);
            //Debug.Log(transform.GetComponent<SpriteRenderer>().sprite.name + "��ݒu�����I");
        }
    }
    public void CancelChoose()
    {
        //Debug.Log(transform.GetComponent<SpriteRenderer>().sprite.name + " : " + transform.gameObject.name + "�L�����Z���֐��ɓ�����");
        if (isTouched == true)
        {
            Debug.Log(transform.GetComponent<SpriteRenderer>().sprite.name + "�̓X�^���o�C���L�����Z�����܂����I");
            isTouched = false;
        }
    }
    public void GetTex(int n)
    {
        SpriteRenderer s = GetComponent<SpriteRenderer>();
        s.sprite = sprites[n];
    }
}
