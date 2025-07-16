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
    [SerializeField] Vector2 viewportPosition = new Vector2(0.2f, 0.8f); // 画面内の相対位置（右下なら例: (0.7, 0.3)）
    private void Update()
    {
        Camera cam = Camera.main;
        Vector3 worldPos = cam.ViewportToWorldPoint(new Vector3(viewportPosition.x, viewportPosition.y, 0));
        transform.position = new Vector3(worldPos.x, worldPos.y, transform.position.z);
    }
    public void SetTile()
    {
        me = Instantiate(transform.gameObject, transform.position, Quaternion.identity);
        mySr = me.GetComponent<SpriteRenderer>();
        Debug.Log(sp[0].name);
        mySr.sprite = sp[0];
        me.GetComponent<Transform>().localScale = new Vector3(2.7f, 2.7f, 0);
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
    public void ZoomIn()
    {
        me.GetComponent<Transform>().localScale = new Vector3(0.81f, 0.81f, 0);
    }
    public void ZoomOut()
    {
        me.GetComponent<Transform>().localScale = new Vector3(2.7f, 2.7f, 0);
    }
}