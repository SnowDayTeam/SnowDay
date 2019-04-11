using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour {

    public Sound[] sounds;
    public static AudioManager instance;
    [SerializeField]
    private bool DontDestroy = false;

	void Awake () {
        //singleton check, if exists, destroy new instance,exit script
        if (instance == null)
            instance = this;
        else {
            Destroy(gameObject);
            return;
        }

        if (DontDestroy) {
            DontDestroyOnLoad(gameObject);
        }
        foreach (Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.looping;
        }
	}
    //can play music on start of level, will keep audio playing if using DontDestoryOnLoad, if different music for seperate game modes, cant use DDOL
    private void Start()
    {
        // Play("Music");
    
    }

    //call this to play an audio file
    public void Play(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) {
            Debug.Log("AUDIO SOURCE: " + name + " NOT VALID");
            return;
        }
            s.source.Play();

            //----- Debug--------------
            Debug.Log("Did Play " + name);

            //-------------------

    }
    //call this to play an audio file
    public void PlayOnce(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("AUDIO SOURCE: " + name + " NOT VALID");
            return;
        }
        s.source.PlayOneShot(s.clip);

        //----- Debug--------------
        Debug.Log("Did Play " + name);

        //-------------------

    }
    //call this to pause an audio file
    public void PauseAudio(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("AUDIO SOURCE: " + name + " NOT VALID");
            return;
        }
        s.source.Pause();
    }
    //call this to pause an audio file
    public void UnPauseAudio(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("AUDIO SOURCE: " + name + " NOT VALID");
            return;
        }
        s.source.UnPause();
    }
    //call this to stop an audio file
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("AUDIO SOURCE: " + name + " NOT VALID");
            return;
        }
        s.source.Stop();
    }
}
