using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleTextShower : MonoBehaviour
{
    string[] titleSpell = { "E", "l", "e", "m", "e", "n", "t", "a", "l"," ", "E", "x", "p", "e", "r", "i", "m", "e", "n", "t" };
    Text text;
    void Start()
    {
        text = transform.GetComponent<Text>();
        text.text = "";
    }
    public void TitleShow()
    {
        foreach (string spell in titleSpell)
        {
            text.text += spell;
        }
    }
}
