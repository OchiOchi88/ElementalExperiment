using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndWarning : MonoBehaviour
{
    void Update()
    {

    }
    public void StartGame()
    {
        Initiate.Fade("TitleScene", new Color(0, 0, 0), 1.0f);
    }
}
