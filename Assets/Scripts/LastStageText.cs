using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LastStageText : MonoBehaviour
{
    public void LastStageClear()
    {
        Text text = transform.GetComponentInChildren<Text>();
        text.fontSize = 25;
        text.text = "���X�g�X�e�[�W\n�N���A�I\n���߂łƂ��I";
        Button btn = transform.GetComponent<Button>();
        btn.interactable = false;
    }
}
