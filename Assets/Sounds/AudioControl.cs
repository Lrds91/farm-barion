using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioControl : MonoBehaviour
{
    [SerializeField] private AudioClip bgmMusic;
    private bool soundState = true;

    [SerializeField] private AudioMixer mixer;
    [SerializeField] private AudioSource audioSource;

    private AudioManager audioM;
    
    void Start()
    {
        audioM = FindObjectOfType<AudioManager>();
        audioM.PlayBGM(bgmMusic);
    }


    public void OnChangeSlider(float value)
    {
        audioSource.volume = value;
    }

}
