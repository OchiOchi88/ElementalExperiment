using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectingTab : MonoBehaviour
{
    [SerializeField] Vector2 viewportPosition = new Vector2(0.2f, 0.8f); // 画面内の相対位置（右下なら例: (0.7, 0.3)）
    [SerializeField] float zOffset = 10f; // カメラとの距離（Orthographicの場合は0でOK）
    void Update()
    {
        Camera cam = Camera.main;
        Vector3 worldPos = cam.ViewportToWorldPoint(new Vector3(viewportPosition.x, viewportPosition.y, zOffset));
        transform.position = new Vector3(worldPos.x, worldPos.y, transform.position.z);
    }
}
