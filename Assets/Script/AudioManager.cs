using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;
    Sound BGM;
    void Awake()
    {
        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;

            sound.source.playOnAwake = sound.playOnAwake;
            sound.source.loop = sound.loop;
            sound.source.volume = sound.volume;
        }

    }

    void Start()
    {
        BGM = Array.Find(sounds, sound => sound.name == "Background"); 
        //Play("Background");
        BGM.source.Play();
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s != null)
            s.source.Play();
    }

    public void StopPlayBG()
    {
        BGM.source.Pause();
    }

    public void StartPlayBG()
    {
        BGM.source.Play();
    }



}
