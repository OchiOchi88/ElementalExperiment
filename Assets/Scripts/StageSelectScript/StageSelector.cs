using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelector : MonoBehaviour
{
    [SerializeField] GameObject stageButton;
    public float maxStage = 4;
    void Start()
    {
        Vector3 pos;
        for (int j = 0; j < maxStage / 10; j++)
        {
            float k;
            if(j >= maxStage/10 -1)
            {
                k = maxStage % 10;
            }
            else
            {
                k = 10;
            }
            for (int i = 1; i <= k; i++)
            {
                if (i > 5)
                {
                    pos = new Vector3((i * 150) - 1200, 0, 0);
                }
                else
                {
                    pos = new Vector3((i * 150) - 450, -100, 0);
                }
                GameObject clone = Instantiate(stageButton, pos, Quaternion.identity);
                clone.GetComponent<StageSetter>().SetStage(i);
            }
        }
    }
}
