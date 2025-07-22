using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TileCollider : MonoBehaviour
{
    Sprite mySprite;
    int number = 0;
    bool standBy = false;
    bool isTouched = false;
    [SerializeField] TileSetter ts;
    [SerializeField] TilePieceManager tpm;
    List <GameObject> victimTile;
    // Start is called before the first frame update
    void Start()
    {
        mySprite = GetComponent<SpriteRenderer>().sprite;
        GetComponent<BoxCollider2D>();
    }
    public void Restart()
    {
        GetComponent<SpriteRenderer>().sprite = mySprite;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {

        Vector3 elemPos = collision.transform.position;
        Vector3 tilePos = transform.position;
        float dis = Vector3.Distance(elemPos, tilePos);
        //Debug.Log("スクリプト到達！");
        if(dis >= 0.2f)
        {
            collision.GetComponent<ElementMover>().SetGimicEnd();
        }
        if (dis <= 0.025f)
        {
            Sprite myTile = transform.GetComponent<SpriteRenderer>().sprite;
            if (myTile.name == "tile10")
            {
                //Debug.Log("10到達！");
                ElementMover dir = collision.GetComponent<ElementMover>();
                dir.ChangeDirection(1);
            }
            if (myTile.name == "tile11")
            {
                //Debug.Log("11到達！");
                ElementMover dir = collision.GetComponent<ElementMover>();
                dir.ChangeDirection(2);
            }
            if (myTile.name == "tile12")
            {
                //Debug.Log("12到達！");
                ElementMover dir = collision.GetComponent<ElementMover>();
                dir.ChangeDirection(3);
            }
            if (myTile.name == "tile13")
            {
                //Debug.Log("13到達！");
                ElementMover dir = collision.GetComponent<ElementMover>();
                dir.ChangeDirection(4);
            }if (myTile.name == "tile31")
            {
                ElementMover dir = collision.GetComponent<ElementMover>();
                int direction = dir.GetDirection();
                if (direction == 4)
                {
                    dir.ChangeDirection(1);
                }
                else
                {
                    dir.ChangeDirection(direction + 1);
                }
            }
            if (myTile.name == "tile32")
            {
                ElementMover dir = collision.GetComponent<ElementMover>();
                int direction = dir.GetDirection();
                if (direction == 1)
                {
                    dir.ChangeDirection(4);
                }
                else
                {
                    dir.ChangeDirection(direction - 1);
                }
            }
            if (myTile.name == "tile41")
            {
                ElementMover dir = collision.GetComponent<ElementMover>();
                int direction = dir.GetDirection();
                if(direction >= 3)
                {
                    direction -= 2;
                }
                else
                {
                    direction += 2;
                }
                    dir.ChangeDirection(direction);
            }
            if (myTile.name == "tile51")
            {
                Sprite getTile = FindObjectOfType<TileManager>().UpdateTile(15);
                GetComponent<SpriteRenderer>().sprite = getTile;
            }
            if (myTile.name == "tile52")
            {
                Sprite getTile = FindObjectOfType<TileManager>().UpdateTile(16);
                GetComponent<SpriteRenderer>().sprite = getTile;
            }
            if (myTile.name == "tile53")
            {
                Sprite getTile = FindObjectOfType<TileManager>().UpdateTile(17);
                GetComponent<SpriteRenderer>().sprite = getTile;
            }
            if (myTile.name == "tile54")
            {
                Sprite getTile = FindObjectOfType<TileManager>().UpdateTile(18);
                GetComponent<SpriteRenderer>().sprite = getTile;
            }
            if (myTile.name == "tile55")
            {
                Sprite getTile = FindObjectOfType<TileManager>().UpdateTile(19);
                GetComponent<SpriteRenderer>().sprite = getTile;
                victimTile = GetObjectsOutOfCharge();
                ExecuteOutOfCharge(victimTile);
            }
            if (myTile.name == "tileFF")
            {
                //Debug.Log("ステージ失敗...！");
                ElementMover em = collision.GetComponent<ElementMover>();
                em.Defeat();
            }
        }
    }
    List<GameObject> GetObjectsOutOfCharge()
    {
        List<GameObject> objects = new List<GameObject>();

        // 検出範囲のサイズと中心位置
        Vector2 size = new Vector2(1.5f, 1.5f);
        Vector2 center = transform.position;

        // 2DのOverlapBoxを使用
        Collider2D[] colliders = Physics2D.OverlapBoxAll(center, size, 0f);

        Debug.Log("対象タイル個数：" + colliders.Length);

        foreach (Collider2D collider in colliders)
        {
            if (collider.tag=="Tile")
            {
                objects.Add(collider.gameObject);
            }
        }

        return objects;
    }
    // 指定した関数を実行
    void ExecuteOutOfCharge(List<GameObject> objects)
    {
        foreach (GameObject obj in objects)
        {
            obj.GetComponent<TileCollider>().OutOfCharge();
        }
    }

    // 実行する関数の例
    void OutOfCharge()
    {
        Sprite myTile= GetComponent<TileManager>().UpdateTile(1);
        GetComponent<SpriteRenderer>().sprite = myTile;
    }
    public void SetVoid(int i)
    {
        //Debug.Log(i + "番受信");
        number = i;
    }
    public void StandBy()
    {
        //Debug.Log(number);

        if (number != 0)
        {
            //Debug.Log(number + ":スタンバイ関数に入った");
            if (standBy == true)
            {
                tpm.CancelChoose();
            }
            else
            {
                standBy = true;
            }
        }
    }
    public void OnMouseDown()
    {
        Debug.Log(number+"番に設置しようとした(状態："+standBy+")");
        if(standBy == true && number != 0)
        {
            isTouched = true;
            tpm.GetSR();
        }
    }
    public void GetsRenderer(Sprite sr)
    {
        if (isTouched == true)
        {
            //Debug.Log("Result:"+sr);
            transform.GetComponent<SpriteRenderer>().sprite = sr;
            mySprite = GetComponent<SpriteRenderer>().sprite;
            isTouched = false;
        }
    }
    public bool GetStand()
    {
        return isTouched;
    }
    public void Delete()
    {
        Destroy(transform.gameObject);
    }
}
