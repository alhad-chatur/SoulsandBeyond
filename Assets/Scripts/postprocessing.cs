using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System;
public class postprocessing : MonoBehaviour
{
    // public Sound[] sounds;
    // bool cycle = true;
    // private bool cycle2 = true;
    public AudioSource sour;
    
    Volume vol;
    public float maxdistance = 20.0f;
    public float startoffset = 5.0f;
    public GameObject player1;
    public GameObject player2;
    float inidist;
    void Start()
    {
        vol = GetComponent<Volume>();
        inidist = Mathf.Abs(player1.transform.position.x - player2.transform.position.x);
        //inidist = Vector3.Distance(player1.transform.position, player2.transform.position);
        
        
        
        // sour = Array.Find(sounds, sound => sound.name == "heartbeat").source;
        
        
    }
    // Update is called once per frame
    void Update()
    {
        if(dmanager.isdying1 == false && dmanager.isdying2 == false)
        {
            float dist = Mathf.Abs(player1.transform.position.x - player2.transform.position.x) - inidist - startoffset;

            if (dist > 0)
            {
                if (dist > maxdistance)
                    Debug.Log("death");
                vol.weight = dist / maxdistance;
            }
            else
                vol.weight = 0;



            if (vol.weight > 0.2 && !sour.isPlaying )
            {
                // Sound s = Array.Find(sounds, sound => sound.name == "theam");
                // s.volume = 0.1f;
                FindObjectOfType<audio_manager>().play("heartbeat",2.5f * vol.weight+0.5f);
                
                // if (!source.isPlaying && cycle)
                // {
                //     FindObjectOfType<audio_manager>().play("heartbeat");
                //     cycle = false;
                //     cycle2 = true;
                //
                // }
                // if (!source.isPlaying && cycle2)
                // {
                //     FindObjectOfType<audio_manager>().play("no sound",2*vol.weight+0.2f);
                //     cycle = true;
                //     cycle2 = false;
                //
                // }


            }
        }
    }
    
    
    
}
