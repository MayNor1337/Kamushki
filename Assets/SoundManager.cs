using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource _audio;

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
    }

    public void SetAudio(AudioClip clip)
    {
        _audio.clip = clip;
        _audio.Play();
    }
}
