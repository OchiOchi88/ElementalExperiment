using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectingTab : MonoBehaviour
{
    [SerializeField] Vector2 viewportPosition = new Vector2(0.2f, 0.8f); // ��ʓ��̑��Έʒu�i�E���Ȃ��: (0.7, 0.3)�j
    [SerializeField] float zOffset = 10f; // �J�����Ƃ̋����iOrthographic�̏ꍇ��0��OK�j
    void Update()
    {
        Camera cam = Camera.main;
        Vector3 worldPos = cam.ViewportToWorldPoint(new Vector3(viewportPosition.x, viewportPosition.y, zOffset));
        transform.position = new Vector3(worldPos.x, worldPos.y, transform.position.z);
    }
}
