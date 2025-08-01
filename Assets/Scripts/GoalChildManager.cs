using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalChildManager : MonoBehaviour
{
    [SerializeField] Sprite[] ElementSprite;
    SpriteRenderer sr;
    int color;
    public void Goal(int c)
    {
        sr = transform.GetComponent<SpriteRenderer>();
        Debug.Log("化学反応成功！");    
        sr.sprite = ElementSprite[ElementSprite.Length -1];
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Element")
        {
            Debug.Log("ゴールは元素に触れました！");
            ElementMover mover = collision.GetComponent<ElementMover>();
            GoalChildManager thisGoal = transform.GetComponent<GoalChildManager>();
            int c = mover.IsGoal();
            if (c == color)
            {
                mover.Goaled(thisGoal);
                GameCtrler gc = FindObjectOfType<GameCtrler>();
                gc.Goal();
            }
        }
    }
    public void SetColor(int c)
    {
        color = c;
    }
    public void Restart()
    {
        Destroy(transform.gameObject);
    }
}
