using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceForPref : MonoBehaviour
{
    public GameObject objectMusic;
    private float musicVolume;
    private AudioSource audioSource;
    private bool isSetVolume = false;
    // Start is called before the first frame update
    void Start()
    {
        objectMusic = GameObject.FindGameObjectWithTag("GameMusic");
        audioSource = objectMusic.GetComponent<AudioSource>();

        if (isSetVolume)
        {
            musicVolume = PlayerPrefs.GetFloat("volume");
            audioSource.volume = musicVolume;
        }
    }
}
