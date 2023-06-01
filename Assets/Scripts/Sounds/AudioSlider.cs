using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioSlider : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioMixMode mixMode;

    public void OnChangeSlider(float value)
    {
        switch(mixMode)
        {
            case AudioMixMode.LogrithmicMixerVolume:
                mixer.SetFloat("Volume", Mathf.Log10(value) * 20);
                PlayerPrefs.SetFloat("Volume", value);
                PlayerPrefs.Save();
                break;
        }
    }

    public enum AudioMixMode
    {
        LogrithmicMixerVolume
    }
}
