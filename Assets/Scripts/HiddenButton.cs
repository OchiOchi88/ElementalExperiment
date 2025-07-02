using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenButton : MonoBehaviour
{
    void Start()
    {
        transform.localScale = new Vector3(0, 0, 0);
    }
    public void SetTrue()
    {
        transform.localScale = new Vector3(1, 1, 1);
    }
    public void SetFalse()
    {
        transform.localScale = new Vector3(0, 0, 0);
    }
}
