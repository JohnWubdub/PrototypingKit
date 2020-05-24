using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is a simple audio manager that lets you assign a set of tracks to an array in the inspector and play them from code.
//You can play a clip once or on loop. 
//Call a track using its index in the array, eg: PlayAudioClip(0) or PlayAudioLoop(4)
//You can also play a random clip from a range in the array, eg: PlayAudioClip(0,2) will play either the 0, 1, or 2 tracks.
//Stop all audio using StopAllAudio()

//Original script by @srgovindan
public class AudioManager : MonoBehaviour
{
    public static AudioManager AM;
    public List<AudioClip> audioClips;
    private AudioSource _audioSource;

    void Awake()
    {
        // PSUEDO-SINGLETON
        // This gameobject does NOT persist on load
        if (AM == null)
        {
            //DontDestroyOnLoad(gameObject);
            AM = this;
            _audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayAudioClip(int i, float volume=1f)
    {
        //Debug.Log("Playing Audio Clip " + i);
        _audioSource.PlayOneShot(audioClips[i],volume);
    }
       
    //Random clip from range
    public void PlayAudioClip(int i, int j, float volume=1f)
    {
        int k = Random.Range(i, j + 1);
        //Debug.Log("Playing Audio Clip " + i);
        _audioSource.PlayOneShot(audioClips[k],volume);
    }
    
    public void PlayAudioLoop(int i, float volume=1f)
    {
        //Debug.Log("Playing Audio Clip " + i);
        _audioSource.loop = true;
        _audioSource.clip = audioClips[i];
        _audioSource.volume = volume;
        _audioSource.Play();
    }
       
    //Random clip from range
    public void PlayAudioLoop(int i, int j, float volume=1f)
    {
        int k = Random.Range(i, j + 1);
        //Debug.Log("Playing Audio Clip " + i);
        _audioSource.loop = true;
        _audioSource.clip = audioClips[k];
        _audioSource.volume = volume;
        _audioSource.Play();
    }

    public void StopAllAudio()
    {
        _audioSource.Stop();
    }
}
