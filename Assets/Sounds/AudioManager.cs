using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] private AudioSource audioSource;
    private bool soundState = true;

    // Start is called before the first frame update
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LigarDesligarSom()
    {
        soundState = !soundState;
    }

    public void PlayBGM(AudioClip audio)
    {
        audioSource.clip = audio;
        audioSource.Play();
    }

    public void OnChangeSlider(float value)
    {
        audioSource.volume = value;
    }
}
