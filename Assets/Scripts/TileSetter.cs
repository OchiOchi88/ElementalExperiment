using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSetter : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;
    bool ismove = false;
    [SerializeField] GameCtrler gc;
    SpriteRenderer sRenderer;
    [SerializeField] TileCollider tc;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void SetSprite(Sprite sr)
    {
        sRenderer = GetComponent<SpriteRenderer>();
        sRenderer.sprite = sr;
    }
    // Update is called once per frame
    void Update()
    {
    }
    public void Down()
    {
//        this.screenPoint = Camera.main.WorldToScreenPoint(transform.position);
//        this.offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        tc.StandBy();
        ismove = true;
    }
    public void Drag()
    {
        
    }
    public void Up()
    {
    }
    public void GetSR()
    {
        if(ismove == true)
        {
            tc.GetsRenderer(sRenderer);
        }
    }
}
