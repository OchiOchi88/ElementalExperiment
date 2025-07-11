using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSetter : MonoBehaviour
{
    static int stage = 0;
    Color col = new Color(0, 0, 0);
    [SerializeField] Text myText;
    public void SetStage(int number)
    {
        stage = number;
        myText.text = $"{stage}";

    }

    public void StartStage()
    {
        Initiate.Fade("PuzzleScene", col, 1.0f);
    }
}
