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
        Debug.Log("���w���������I");    
        sr.sprite = ElementSprite[ElementSprite.Length -1];
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Element")
        {
            Debug.Log("�S�[���͌��f�ɐG��܂����I");
            ElementMover mover = collision.GetComponent<ElementMover>();
            GoalChildManager thisGoal = transform.GetComponent<GoalChildManager>();
            int c = mover.IsGoal();
            if (c == color)
            {
                mover.Goaled(thisGoal);
            }
        }
    }
    public void SetColor(int c)
    {
        color = c;
    }
}
