using UnityEngine;
using UnityEngine.Audio;
using System;
using System.Reflection;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    private void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            //s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play(string name, float volume)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        try
        {
            if (!s.source.isPlaying)
            {
                s.source.Play();
                Debug.Log(name + " is playing at volume " + volume);
            }
            s.source.volume = volume;
        }
        catch
        {
            Debug.LogWarning("Cannot find sound file: " + name);
        }
    }
}
