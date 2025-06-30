using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCollider : MonoBehaviour
{
    Sprite mySprite;
    int number = 0;
    bool standBy = false;
    [SerializeField]TileSetter ts;
    // Start is called before the first frame update
    void Start()
    {
        mySprite = GetComponent<SpriteRenderer>().sprite;
        GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        Vector3 elemPos = collision.transform.position;
        Vector3 tilePos = transform.position;
        float dis = Vector3.Distance(elemPos, tilePos);
        //Debug.Log("スクリプト到達！");
        if (dis <= 0.1f)
        {

            if (mySprite.name == "tile10")
            {
                Debug.Log("10到達！");
                ElementMover dir = collision.GetComponent<ElementMover>();
                dir.ChangeDirection(1);
            }
            if (mySprite.name == "tile11")
            {
                Debug.Log("11到達！");
                ElementMover dir = collision.GetComponent<ElementMover>();
                dir.ChangeDirection(2);
            }
            if (mySprite.name == "tile12")
            {
                Debug.Log("12到達！");
                ElementMover dir = collision.GetComponent<ElementMover>();
                dir.ChangeDirection(3);
            }
            if (mySprite.name == "tile13")
            {
                Debug.Log("13到達！");
                ElementMover dir = collision.GetComponent<ElementMover>();
                dir.ChangeDirection(4);
            }if(mySprite.name == "tileFF")
            {
                Debug.Log("ステージ失敗...！");
                ElementMover em = collision.GetComponent<ElementMover>();
                em.Defeat();
            }
        }
    }
    public void SetVoid(int i)
    {
        number = i;
    }
    public void StandBy()
    {
        if(number != 0)
        {
            standBy = true;
        }
    }
    public void OnMouseDown()
    {
        if(standBy == true)
        {
            ts.GetSR();
        }
    }
    public void GetsRenderer(SpriteRenderer sr)
    {
        mySprite = sr.sprite;
    }
}
