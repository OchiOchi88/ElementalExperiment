using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class StageSelectManager : MonoBehaviour
{
    int ableStage = 0;
    [SerializeField] StageSelector ss;
    void Start()
    {
        ableStage = GameCtrler.clearStage;
        //ss.StageAble(ableStage);
    }
}
