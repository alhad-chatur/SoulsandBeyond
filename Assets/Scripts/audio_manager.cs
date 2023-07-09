using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audio_manager : MonoBehaviour
{
    public Sound[] sounds;
    public AudioSource source;

    public static audio_manager instance;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        
        foreach (Sound s in sounds)
        {
            
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
        
    }

    private void FixedUpdate()
    {
        foreach (Sound s in sounds)
        {
            
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void play(string soundName, float pitch = 1f)
    {
        Sound s = Array.Find(sounds, sound => sound.name == soundName);
        s.pitch = pitch;
        s.source.Play();
        source = s.source;
    }
    private void Start()
    {
        play("theam");
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
