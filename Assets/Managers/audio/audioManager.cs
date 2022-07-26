using Unity.Audio;
using System;
using UnityEngine;

public class audioManager : MonoBehaviour
{
    public static audioManager main;
    public Sound[] sounds;
    // Start is called before the first frame update
    private void Awake()
    {
        //sets static audio manager
        if (main == null)
        {
            
            main = this;
        }
        else
        {
            
            Destroy(this);
        }
        
    foreach (Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.clip = s.clip;
        }
    }

    public void play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s==null)
        {
            Debug.Log("s == null");
            return;
        }
        s.source.Play();
        
    }
    public void play(string name,float pitch)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s==null)
        {
            Debug.Log("s==null");
            return;
        }
        s.source.pitch = pitch;
        s.source.Play();
    }

   



}
