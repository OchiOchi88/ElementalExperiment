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
    //  �󔒃^�C�����^�b�`���ꂽ�Ȃ玖�O�Ƀp���b�g����^�C�����I�΂�Ă���ΐݒu���鏈��
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
    //  �p���b�g����ق��̃^�C�����I�����ꂽ�Ƃ��ɒ��O�܂őI��ł����^�C���̐ݒu�X�^���o�C���L�����Z�����鏈��
    public void CancelChoose()
    {
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
