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
        //Debug.Log("�X�N���v�g���B�I");
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
            }if(myTile.name == "tileFF")
            {
                //Debug.Log("�X�e�[�W���s...�I");
                ElementMover em = collision.GetComponent<ElementMover>();
                em.Defeat();
            }
        }
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
