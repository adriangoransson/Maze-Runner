using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    private const string AUDIO_KEY = "AUDIO_KEY";

    void Start()
    {
        Toggle t = GetComponent<Toggle>();
        t.onValueChanged.AddListener(Toggled);

        t.isOn = PlayerPrefs.GetInt(AUDIO_KEY, 1) > 0;
    }

    private void Toggled(bool soundOn)
    {
        int volume = soundOn ? 1 : 0;
        AudioListener.volume = volume;
        PlayerPrefs.SetInt(AUDIO_KEY, volume);
        PlayerPrefs.Save();
    }
}
