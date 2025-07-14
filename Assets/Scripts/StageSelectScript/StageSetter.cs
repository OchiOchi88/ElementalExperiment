using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StageSetter : MonoBehaviour
{
    int setStage = 0;
    [SerializeField] Text myText;
    [SerializeField] StageSelector ss;
    void Start()
    {
        if (ss == null)
        {
            ss = FindObjectOfType<StageSelector>();
        }
    }
    public void SetStage(int number)
    {
        setStage = number;
        myText.text = $"{setStage}";

    }
    public void SetPos(Vector3 pos)
    {
        RectTransform rect = transform.GetComponent<RectTransform>();
        rect.transform.position = pos;
    }
    public void SendStage()
    {
        ss.StartStage(setStage);
    }
    public void Delete()
    {
        Destroy(transform.gameObject);
    }
}
