using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileTab : MonoBehaviour
{
    [SerializeField] Vector2 viewportPosition = new Vector2(0.85f, 0.0f); // 画面内の相対位置（右下なら例: (0.7, 0.3)）
    [SerializeField] float zOffset = 0; // カメラとの距離（Orthographicの場合は0でOK）
    int zoom = 0;
    void Update()
    {
        Camera cam = Camera.main;
        Vector3 worldPos = cam.ViewportToWorldPoint(new Vector3(viewportPosition.x, viewportPosition.y, zOffset));
        transform.position = new Vector3(worldPos.x, worldPos.y, transform.position.z);
    }
    public void ZoomIn()
    {
        zoom++;
        GetComponent<Transform>().localScale = new Vector3(0, 0, 0);
    }
    public void ZoomOut()
    {
        zoom--;
        if (zoom == 0)
        {
            GetComponent<Transform>().localScale = new Vector3(5.5f, 1000, 1);
        }
    }
}
