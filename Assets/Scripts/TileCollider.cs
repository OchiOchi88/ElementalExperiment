using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TileCollider : MonoBehaviour
{
    float detectionRadius = 1.1f; // ���o���a
    string targetTag = "UI"; // ���o�Ώۂ̃^�O
    string functionName = "OutOfCharge"; // ���s����֐���
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
    private void OnTriggerStay2D(Collider2D collision)
    {

        Vector3 elemPos = collision.transform.position;
        Vector3 tilePos = transform.position;
        float dis = Vector3.Distance(elemPos, tilePos);
        //Debug.Log("�X�N���v�g���B�I");
        if(dis >= 0.2f)
        {
            collision.GetComponent<ElementMover>().SetGimicEnd();
        }
        if (dis <= 0.1f)
        {
            Sprite myTile = transform.GetComponent<SpriteRenderer>().sprite;
            if (myTile.name == "tile10")
            {
                //Debug.Log("10���B�I");
                ElementMover dir = collision.GetComponent<ElementMover>();
                dir.ChangeDirection(1);
            }
            if (myTile.name == "tile11")
            {
                //Debug.Log("11���B�I");
                ElementMover dir = collision.GetComponent<ElementMover>();
                dir.ChangeDirection(2);
            }
            if (myTile.name == "tile12")
            {
                //Debug.Log("12���B�I");
                ElementMover dir = collision.GetComponent<ElementMover>();
                dir.ChangeDirection(3);
            }
            if (myTile.name == "tile13")
            {
                //Debug.Log("13���B�I");
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
                mySprite = FindObjectOfType<TileManager>().UpdateTile(15);
            }
            if (myTile.name == "tile52")
            {
                mySprite = FindObjectOfType<TileManager>().UpdateTile(16);
            }
            if (myTile.name == "tile53")
            {
                mySprite = FindObjectOfType<TileManager>().UpdateTile(17);
            }
            if (myTile.name == "tile54")
            {
                mySprite = FindObjectOfType<TileManager>().UpdateTile(18);
            }
            if (myTile.name == "tile55")
            {
                mySprite = FindObjectOfType<TileManager>().UpdateTile(19);
                victimTile = GetObjectsWithinRadius();
                ExecuteFunctionOnObjects(victimTile);
            }
            if (myTile.name == "tileFF")
            {
                //Debug.Log("�X�e�[�W���s...�I");
                ElementMover em = collision.GetComponent<ElementMover>();
                em.Defeat();
            }
        }
    }
    List<GameObject> GetObjectsWithinRadius()
    {
        List<GameObject> objects = new List<GameObject>();
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag(targetTag))
            {
                objects.Add(collider.gameObject);
            }
        }
        return objects;
    }
    // �w�肵���֐������s
    void ExecuteFunctionOnObjects(List<GameObject> objects)
    {
        foreach (GameObject obj in objects)
        {
            obj.SendMessage(functionName, SendMessageOptions.DontRequireReceiver);
        }
    }

    // ���s����֐��̗�
    void OutOfCharge()
    {
        mySprite = GetComponent<TileManager>().UpdateTile(1);
    }
    public void SetVoid(int i)
    {
        //Debug.Log(i + "�Ԏ�M");
        number = i;
    }
    public void StandBy()
    {
        //Debug.Log(number);

        if (number != 0)
        {
            //Debug.Log(number + ":�X�^���o�C�֐��ɓ�����");
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
        Debug.Log(number+"�Ԃɐݒu���悤�Ƃ���(��ԁF"+standBy+")");
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
