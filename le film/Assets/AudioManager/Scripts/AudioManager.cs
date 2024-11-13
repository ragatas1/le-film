using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{

    /* Dette scriptet trenger bare å ligge på et
    eget game object, for ordens skyld. */

    /* Bruk 'AudioManager.Play("lydnavn");' på
    eventene du vil at lyden skal spilles av. */

    /* Bruk 'AudioManager.Stop("lydnavn");' på
    eventene du vil at lyden skal stoppes. */

    public Sound[] sounds;


    public static AudioManager instance;

    void Awake()
    {
        if (instance != null)

        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.loop = s.loop;
            s.source.pitch = s.pitch = 1f;


        }
    }

    public static void Play(string name) => instance.PlayInstance(name);

    public void PlayInstance (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {  
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        if(s.randomPitch)
        {
            s.source.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
        }

        if(s.greaterRandomPitch)
        {
            s.source.pitch = UnityEngine.Random.Range(0.7f, 1.3f);
        }

        s.source.Play();
    }

    public static void Stop(string name) => instance.StopPlayInstance(name);

    public void StopPlayInstance(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.Stop();
    }

}