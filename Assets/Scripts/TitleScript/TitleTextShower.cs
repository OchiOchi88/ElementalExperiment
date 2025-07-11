using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleTextShower : MonoBehaviour
{
    string[] titleSpell = { "E", "l", "e", "m", "e", "n", "t", "a", "l","\n", "E", "x", "p", "e", "r", "i", "m", "e", "n", "t" };
    Text text;
    [SerializeField] TitleController tc;
    void Start()
    {
        text = transform.GetComponent<Text>();
        text.text = "";
        StartCoroutine("TitleWait");
    }
    IEnumerator TitleWait()
    {
        yield return new WaitForSeconds(0.5f);
            StartCoroutine("TitleWrite");

    }
    IEnumerator TitleWrite()
    {
        foreach (string spell in titleSpell) {
            text.text += spell;
            yield return new WaitForSeconds(0.05f);
        }
        tc.ButtonShow();
    }
}
