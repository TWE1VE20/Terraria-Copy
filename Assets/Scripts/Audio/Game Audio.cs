using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] audioClips;

    private void Start()
    {
        audioSource.clip = audioClips[0];
        audioSource.Play();
        StartCoroutine(ChangeAudioClip());
    }

    IEnumerator ChangeAudioClip()
    {
        while (audioSource.isPlaying)
        {
            yield return null;
        }
        int randomIndex = Random.Range(0, audioClips.Length);
        audioSource.clip = audioClips[randomIndex];
        audioSource.Play();
        StartCoroutine(ChangeAudioClip());
    }
}
