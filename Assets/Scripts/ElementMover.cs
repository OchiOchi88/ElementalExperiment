using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ElementMover : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;
    private Vector3 targetPosition;
    bool ismove = false;
    int moveDir;
    int color;
    [SerializeField] GoalElementManager gElementManager;
    [SerializeField] ElementManager eManager;
    [SerializeField] Sprite[] ElementSprite;


    public void StartMove(int dir)
    {
        Debug.Log("StartMove: " + dir);
        //        Debug.Log("�����Ɠ����Ă���");
        moveDir = dir;
    }
    void Update()
    {
        if (ismove == true)
        {
            //Debug.Log("�ړ���->�����F" + moveDir);
            targetPosition = transform.position;
            switch (moveDir)
            {
                case 0:
                    return;
                case 1:
                    targetPosition.y = transform.position.y + 0.005f;
                    transform.position = targetPosition;
//                    sr.transform.position = transform.position;
                    
                    if (transform.position.x > 20.0f || transform.position.x < -20.0f ||
                        transform.position.y > 20.0f || transform.position.y < -20.0f)
                    {
                        Destroy(transform.gameObject);
                    }
                    return;
                case 2:
                    targetPosition.x = transform.position.x + 0.005f;
                    transform.position = targetPosition;
//                    sr.transform.position = transform.position;
                    
                    if (transform.position.x > 20.0f || transform.position.x < -20.0f ||
                        transform.position.y > 20.0f || transform.position.y < -20.0f)
                    {
                        Destroy(transform.gameObject);
                    }
                    return;
                case 3:
                    targetPosition.y = transform.position.y - 0.005f;
                    transform.position = targetPosition;
//                    sr.transform.position = transform.position;

                    if (transform.position.x > 20.0f || transform.position.x < -20.0f ||
                        transform.position.y > 20.0f || transform.position.y < -20.0f)
                    {
                        Destroy(transform.gameObject);
                    }
                    return;
                case 4:
                    targetPosition.x = transform.position.x - 0.005f;
                    transform.position = targetPosition;
//                    sr.transform.position = transform.position;

                    if (transform.position.x > 20.0f || transform.position.x < -20.0f ||
                        transform.position.y > 20.0f || transform.position.y < -20.0f)
                    {
                        Destroy(transform.gameObject);
                    }
                    return;
            }
        }
    }
    void Start()
    {
        GetComponent<CircleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        if (eManager != null)
        {
            sr = eManager.GetColor();
        }
        else
        {
            Debug.LogWarning("eManager ���Z�b�g����Ă��܂���I");
        }
    }
    public void SetManager(ElementManager manager)
    {
        eManager = manager;
    }
    public void ChangeDirection(int dir)
    {
        moveDir = dir;
    }
    public int IsGoal()
    {
        return color;
    }
    public void Goaled(GoalChildManager colGoal)
    {
        SpriteRenderer me = transform.GetComponent<SpriteRenderer>();
        moveDir = 0;
        colGoal.Goal(color);
        me.enabled = false;
    }
    public void SetColor(int c)
    {
        color = c;
    }
    public void StratMove()
    {
        Debug.Log("�w�߂��͂���");
        ismove = true;
    }
    public void Defeat()
    {
        Destroy(transform.gameObject);
        Debug.Log("MISS�I");
    }
}

