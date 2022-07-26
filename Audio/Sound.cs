using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

    [Range(1f,2)]
    public float pitch = 1;

    [Range(0f,1)]
    public float volume = 1;

    [HideInInspector]
    public AudioSource source;
}
