using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalElementManager : MonoBehaviour
{
    [SerializeField] Sprite[] ElementSprite;
    public GameObject prefab;
    SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetElement(int x, int y, int color)
    {
        Vector2 spawnPos = new Vector2(x, y);
        GameObject clone = Instantiate(prefab, spawnPos, Quaternion.identity);
        sr = clone.GetComponent<SpriteRenderer>();
        GoalChildManager goalColor = clone.GetComponent<GoalChildManager>();
        goalColor.SetColor(color);
        if (sr != null)
        {
            sr.sprite = ElementSprite[color];
        }
    }
    //public void Goal(SpriteRenderer isr)
    //{
    //    if(isr == sr)
    //    {
    //        sr.sprite = ElementSprite[5];
    //    }
    //}
    //public void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if(collision.gameObject.tag == "Element")
    //    {
    //        Debug.Log("ÉSÅ[ÉãÇÕå≥ëfÇ…êGÇÍÇ‹ÇµÇΩÅI");
    //        ElementMover mover = collision.GetComponent<ElementMover>();
    //        GoalElementManager thisGoal = transform.GetComponent<GoalElementManager>();
    //        mover.Goaled(thisGoal);
    //    }
    //}
}
