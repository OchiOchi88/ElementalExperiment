using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.U2D.Animation;

public class TilePieceMover : MonoBehaviour
{
    GameObject me;
    SpriteRenderer mySr;
    [SerializeField] Sprite[] sp;
    public void SetTile()
    {
        me = Instantiate(transform.gameObject, transform.position, Quaternion.identity);
        mySr = me.GetComponent<SpriteRenderer>();
        Debug.Log(sp[0].name);
        mySr.sprite = sp[0];
    }
    public void SetTile(Sprite sr)
    {
        mySr.sprite = sr;
    }
    public void ResetTile()
    {
        Destroy(mySr);
        me = Instantiate(transform.gameObject, transform.position, Quaternion.identity);
        mySr = me.GetComponent<SpriteRenderer>();
        Debug.Log(sp[0].name);
        mySr.sprite = sp[0];
    }
}
