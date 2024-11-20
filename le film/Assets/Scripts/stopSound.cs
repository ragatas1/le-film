using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stopSound : MonoBehaviour
{
    //Stop all sounds
    private AudioSource[] allAudioSources;
    private void Start()
    {
        StopAllAudio();
    }

    void StopAllAudio()
    {
        allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach (AudioSource audioS in allAudioSources)
        {
            audioS.Stop();
        }
    }
}
