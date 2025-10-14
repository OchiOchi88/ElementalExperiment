using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigManager : MonoBehaviour
{
    public AudioSource bgmAudioSource;
    public float targetVolume = 0.5f;

    void Start()
    {
        // ゲーム開始時に音量を設定する例
        bgmAudioSource.volume = targetVolume;
    }

    // 必要に応じて音量を変更する関数
    public void ChangeVolume(float newVolume)
    {
        bgmAudioSource.volume = newVolume;
    }
}
