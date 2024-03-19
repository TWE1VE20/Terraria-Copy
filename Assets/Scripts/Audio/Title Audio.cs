using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] audioClips;
    private void Start()
    {
        audioSource.clip = audioClips[0];
        audioSource.Play();
        StartCoroutine(LoopAudioClip());
    }

    IEnumerator LoopAudioClip()
    {
        while (audioSource.isPlaying)
        {
            yield return null;
        }
        audioSource.clip = audioClips[1];
        audioSource.Play();
        StartCoroutine(LoopAudioClip());
    }
}
