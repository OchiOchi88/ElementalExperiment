using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSetter : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;
    bool ismove = false;
    [SerializeField] GameCtrler gc;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void SetSprite(Sprite sr)
    {
        SpriteRenderer sRenderer = GetComponent<SpriteRenderer>();
        sRenderer.sprite = sr;
    }
    // Update is called once per frame
    void Update()
    {
        if(ismove == true)
        {
            Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint);
            transform.position = currentPosition;
        }
    }
    public void Down()
    {
        this.screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        this.offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        ismove = true;
    }
    public void Drag()
    {
        
    }
    public void Up()
    {
        float dis;
        ismove = false;
        dis = gc.GetDis(transform.position);
        if(dis < 1.1f)
        {
            
        }
        else
        {
            Destroy(transform.gameObject);
        }
    }
}
