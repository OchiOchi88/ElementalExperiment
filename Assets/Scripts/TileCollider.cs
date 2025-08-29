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
        //  �Ԃ������Ȃɂ��ƃ^�C���̋����𑪒�
        Vector3 elemPos = collision.transform.position;
        Vector3 tilePos = transform.position;
        float dis = Vector3.Distance(elemPos, tilePos);

        //  �Ԃ������̂����f�ȊO�Ȃ�I��
        if (collision.tag != "Element")
        {
            return;
        }
        //  ����̑傫�����^�񒆂�肸��Ă�����I��
        if(dis >= 0.2f)
        {
            collision.GetComponent<ElementMover>().SetGimicEnd();
        }
        //  �߂�������^�C���𓥂񂾔���ɂ���
        if (dis <= 0.025f)
        {
            Sprite myTile = transform.GetComponent<SpriteRenderer>().sprite;
            if (myTile.name == "tile10")
            {
                //  �����]��(��)
                ElementMover dir = collision.GetComponent<ElementMover>();
                dir.ChangeDirection(1);
            }
            if (myTile.name == "tile11")
            {
                //  �����]��(�E)
                ElementMover dir = collision.GetComponent<ElementMover>();
                dir.ChangeDirection(2);
            }
            if (myTile.name == "tile12")
            {
                //  �����]��(��)
                ElementMover dir = collision.GetComponent<ElementMover>();
                dir.ChangeDirection(3);
            }
            if (myTile.name == "tile13")
            {
                //  �����]��(��)
                ElementMover dir = collision.GetComponent<ElementMover>();
                dir.ChangeDirection(4);
            }
            if (myTile.name == "tile20")
            {
                //  �N���ςݒn���𓥂�
                ElementMover em = collision.GetComponent<ElementMover>();
                em.Defeat();
            }
            if (myTile.name == "tile21")
            {
                //  ����1�񓥂߂�n���𓥂�
                Sprite getTile = FindObjectOfType<TileManager>().UpdateTile(6);
                GetComponent<SpriteRenderer>().sprite = getTile;
            }
            if (myTile.name == "tile22")
            {
                //  ����2�񓥂߂�n���𓥂�
                Sprite getTile = FindObjectOfType<TileManager>().UpdateTile(7);
                GetComponent<SpriteRenderer>().sprite = getTile;
            }
            if (myTile.name == "tile23")
            {
                //  ����3�񓥂߂�n���𓥂�
                Sprite getTile = FindObjectOfType<TileManager>().UpdateTile(8);
                GetComponent<SpriteRenderer>().sprite = getTile;
            }
            if (myTile.name == "tile24")
            {
                //  ����4�񓥂߂�n���𓥂�
                Sprite getTile = FindObjectOfType<TileManager>().UpdateTile(9);
                GetComponent<SpriteRenderer>().sprite = getTile;
            }
            if (myTile.name == "tile25")
            {
                //  ����5�񓥂߂�n���𓥂�
                Sprite getTile = FindObjectOfType<TileManager>().UpdateTile(10);
                GetComponent<SpriteRenderer>().sprite = getTile;
            }
            if (myTile.name == "tile31")
            {
                //  ���v���90�x�����]��
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
                //  �����v���90�x�����]��
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
                //  U�^�[��
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
                //  �[�d����(100%->80%)
                Sprite getTile = FindObjectOfType<TileManager>().UpdateTile(16);
                GetComponent<SpriteRenderer>().sprite = getTile;
            }
            if (myTile.name == "tile52")
            {
                //  �[�d����(80%->60%)
                Sprite getTile = FindObjectOfType<TileManager>().UpdateTile(17);
                GetComponent<SpriteRenderer>().sprite = getTile;
            }
            if (myTile.name == "tile53")
            {
                //  �[�d����(60%->40%)
                Sprite getTile = FindObjectOfType<TileManager>().UpdateTile(18);
                GetComponent<SpriteRenderer>().sprite = getTile;
            }
            if (myTile.name == "tile54")
            {
                //  �[�d����(40%->20%)
                Sprite getTile = FindObjectOfType<TileManager>().UpdateTile(19);
                GetComponent<SpriteRenderer>().sprite = getTile;
            }
            if (myTile.name == "tile55")
            {
                //  �[�d�����Ղ��������͂̃^�C���̃M�~�b�N������
                Sprite getTile = FindObjectOfType<TileManager>().UpdateTile(20);
                GetComponent<SpriteRenderer>().sprite = getTile;
                victimTile = GetObjectsOutOfCharge();
                ExecuteOutOfCharge(victimTile);
            }
            if (myTile.name == "tile60")
            {
                //  �G���W�j�A�^�C���𓥂�
                tManager.GimicChange();
            }
            if (myTile.name == "tileFF")
            {
                //  �󔒃^�C���𓥂�Ō��f�����Ă��܂���
                ElementMover em = collision.GetComponent<ElementMover>();
                em.Defeat();
            }
        }
    }
    List<GameObject> GetObjectsOutOfCharge()
    {
        List<GameObject> objects = new List<GameObject>();

        // ���o�͈͂̃T�C�Y�ƒ��S�ʒu
        Vector2 size = new Vector2(1.5f, 1.5f);
        Vector2 center = transform.position;

        // 2D��OverlapBox���g�p
        Collider2D[] colliders = Physics2D.OverlapBoxAll(center, size, 0f);

        Debug.Log("�Ώۃ^�C�����F" + colliders.Length);

        foreach (Collider2D collider in colliders)
        {
            if (collider.tag=="Tile")
            {
                objects.Add(collider.gameObject);
            }
        }

        return objects;
    }
    // �w�肵���֐������s
    void ExecuteOutOfCharge(List<GameObject> objects)
    {
        foreach (GameObject obj in objects)
        {
            obj.GetComponent<TileCollider>().OutOfCharge();
        }
    }

    // ���s����֐��̗�
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
        Debug.Log(number+"�Ԃɐݒu���悤�Ƃ���(��ԁF"+standBy+")");
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
    //  �G���W�j�A�^�C�������܂ꂽ�Ƃ��̃M�~�b�N�̃`�F���W����
    public void Change()
    {
        Sprite myTile = transform.GetComponent<SpriteRenderer>().sprite;
        //  tile10�`13�͕������]
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
        //  tile20�`25�͒n���̕s���񐔂�1����
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
        //  ���v���A�����v���^�C���͋t�]
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
