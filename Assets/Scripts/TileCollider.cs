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
    [SerializeField] TileSetter tileSetter;
    [SerializeField] TilePieceManager tilePieceManager;
    List <GameObject> victimTile;
    [SerializeField] TileManager tManager;
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
        //  ぶつかったなにかとタイルの距離を測定
        Vector3 elemPos = collision.transform.position;
        Vector3 tilePos = transform.position;
        float dis = Vector3.Distance(elemPos, tilePos);

        //  ぶつかったのが元素以外なら終了
        if (collision.tag != "Element")
        {
            return;
        }
        //  所定の大きさ分真ん中よりずれていたら終了
        if(dis >= 0.2f)
        {
            collision.GetComponent<ElementMover>().SetGimicEnd();
        }
        //  近かったらタイルを踏んだ判定にする
        if (dis <= 0.025f)
        {
            Sprite myTile = transform.GetComponent<SpriteRenderer>().sprite;
            if (myTile.name == "tile10")
            {
                //  方向転換(上)
                ElementMover dir = collision.GetComponent<ElementMover>();
                dir.ChangeDirection(1);
            }
            if (myTile.name == "tile11")
            {
                //  方向転換(右)
                ElementMover dir = collision.GetComponent<ElementMover>();
                dir.ChangeDirection(2);
            }
            if (myTile.name == "tile12")
            {
                //  方向転換(下)
                ElementMover dir = collision.GetComponent<ElementMover>();
                dir.ChangeDirection(3);
            }
            if (myTile.name == "tile13")
            {
                //  方向転換(左)
                ElementMover dir = collision.GetComponent<ElementMover>();
                dir.ChangeDirection(4);
            }
            if (myTile.name == "tile20")
            {
                //  起動済み地雷を踏む
                ElementMover em = collision.GetComponent<ElementMover>();
                em.Defeat();
            }
            if (myTile.name == "tile21")
            {
                //  あと1回踏める地雷を踏む
                Sprite getTile = FindObjectOfType<TileManager>().UpdateTile(6);
                GetComponent<SpriteRenderer>().sprite = getTile;
            }
            if (myTile.name == "tile22")
            {
                //  あと2回踏める地雷を踏む
                Sprite getTile = FindObjectOfType<TileManager>().UpdateTile(7);
                GetComponent<SpriteRenderer>().sprite = getTile;
            }
            if (myTile.name == "tile23")
            {
                //  あと3回踏める地雷を踏む
                Sprite getTile = FindObjectOfType<TileManager>().UpdateTile(8);
                GetComponent<SpriteRenderer>().sprite = getTile;
            }
            if (myTile.name == "tile24")
            {
                //  あと4回踏める地雷を踏む
                Sprite getTile = FindObjectOfType<TileManager>().UpdateTile(9);
                GetComponent<SpriteRenderer>().sprite = getTile;
            }
            if (myTile.name == "tile25")
            {
                //  あと5回踏める地雷を踏む
                Sprite getTile = FindObjectOfType<TileManager>().UpdateTile(10);
                GetComponent<SpriteRenderer>().sprite = getTile;
            }
            if (myTile.name == "tile31")
            {
                //  時計回り90度方向転換
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
                //  反時計回り90度方向転換
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
                //  Uターン
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
                //  充電消耗(100%->80%)
                Sprite getTile = FindObjectOfType<TileManager>().UpdateTile(16);
                GetComponent<SpriteRenderer>().sprite = getTile;
            }
            if (myTile.name == "tile52")
            {
                //  充電消耗(80%->60%)
                Sprite getTile = FindObjectOfType<TileManager>().UpdateTile(17);
                GetComponent<SpriteRenderer>().sprite = getTile;
            }
            if (myTile.name == "tile53")
            {
                //  充電消耗(60%->40%)
                Sprite getTile = FindObjectOfType<TileManager>().UpdateTile(18);
                GetComponent<SpriteRenderer>().sprite = getTile;
            }
            if (myTile.name == "tile54")
            {
                //  充電消耗(40%->20%)
                Sprite getTile = FindObjectOfType<TileManager>().UpdateTile(19);
                GetComponent<SpriteRenderer>().sprite = getTile;
            }
            if (myTile.name == "tile55")
            {
                //  充電を消耗しつくし周囲のタイルのギミックを解除
                Sprite getTile = FindObjectOfType<TileManager>().UpdateTile(20);
                GetComponent<SpriteRenderer>().sprite = getTile;
                victimTile = GetObjectsOutOfCharge();
                ExecuteOutOfCharge(victimTile);
            }
            if (myTile.name == "tile60")
            {
                //  エンジニアタイルを踏んだ
                tManager.GimicChange();
            }
            if (myTile.name == "tileFF")
            {
                //  空白タイルを踏んで元素が壊れてしまった
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
        number = i;
    }
    public void StandBy()
    {
        if (number != 0)
        {
            if (standBy == true)
            {
                tilePieceManager.CancelChoose();
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
            tilePieceManager.GetSR();
        }
    }
    public void GetsRenderer(Sprite sr)
    {
        if (isTouched == true)
        {
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
    //  エンジニアタイルが踏まれたときのギミックのチェンジ処理
    public void Change()
    {
        Sprite myTile = transform.GetComponent<SpriteRenderer>().sprite;
        //  tile10～13は方向反転
        if (myTile.name == "tile10")
        {
            Sprite getTile = FindObjectOfType<TileManager>().UpdateTile(4);
            GetComponent<SpriteRenderer>().sprite = getTile;
        }
        if (myTile.name == "tile11")
        {
            Sprite getTile = FindObjectOfType<TileManager>().UpdateTile(5);
            GetComponent<SpriteRenderer>().sprite = getTile;
        }
        if (myTile.name == "tile12")
        {
            Sprite getTile = FindObjectOfType<TileManager>().UpdateTile(2);
            GetComponent<SpriteRenderer>().sprite = getTile;
        }
        if (myTile.name == "tile13")
        {
            Sprite getTile = FindObjectOfType<TileManager>().UpdateTile(3);
            GetComponent<SpriteRenderer>().sprite = getTile;
        }
        //  tile20～25は地雷の不発回数が1増加
        if (myTile.name == "tile20")
        {
            Sprite getTile = FindObjectOfType<TileManager>().UpdateTile(7);
            GetComponent<SpriteRenderer>().sprite = getTile;
        }
        if (myTile.name == "tile21")
        {
            Sprite getTile = FindObjectOfType<TileManager>().UpdateTile(8);
            GetComponent<SpriteRenderer>().sprite = getTile;
        }
        if (myTile.name == "tile22")
        {
            Sprite getTile = FindObjectOfType<TileManager>().UpdateTile(9);
            GetComponent<SpriteRenderer>().sprite = getTile;
        }
        if (myTile.name == "tile23")
        {
            Sprite getTile = FindObjectOfType<TileManager>().UpdateTile(10);
            GetComponent<SpriteRenderer>().sprite = getTile;
        }
        if (myTile.name == "tile24")
        {
            Sprite getTile = FindObjectOfType<TileManager>().UpdateTile(11);
            GetComponent<SpriteRenderer>().sprite = getTile;
        }
        //  時計回り、反時計回りタイルは逆転
        if (myTile.name == "tile31")
        {
            Sprite getTile = FindObjectOfType<TileManager>().UpdateTile(13);
            GetComponent<SpriteRenderer>().sprite = getTile;
        }
        if (myTile.name == "tile32")
        {
            Sprite getTile = FindObjectOfType<TileManager>().UpdateTile(12);
            GetComponent<SpriteRenderer>().sprite = getTile;
        }
    }
}
