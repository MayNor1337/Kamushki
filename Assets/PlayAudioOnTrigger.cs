using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioOnTrigger : MonoBehaviour
{
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private AudioClip clip;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            soundManager.SetAudio(clip);
        }
    }
}
