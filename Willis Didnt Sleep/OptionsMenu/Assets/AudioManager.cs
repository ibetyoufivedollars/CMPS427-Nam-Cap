using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioManager : MonoBehaviour {

    [SerializeField]
    public AudioClip[] music = new AudioClip[10];//songs take up 2 spaces due to possible introloops

    [SerializeField]
    public AudioClip[] sfx = new AudioClip[100];


    public float musicVolume;
    public float sfxVolume;
    float[] loopValue = new float[] { 0, 0, 0, 0, 13.333f, 0, 0, 0, 60.459f, 0 };
    bool isLooping = false;
    int currentSong = 2;//must be an even number!

    // Use this for initialization
    void Start () {
        
        
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    public void LoopSong(AudioSource source, int songId)
    {
        if (source.time == loopValue[songId] && !isLooping)
        {
            source.clip = music[songId + 1];
            currentSong++;
            source.loop = true;
            source.Play();
            isLooping = true;
        }
        maintainSongLoop(source);
    }

    public int getCurrentSong()
    {
        return currentSong;
    }

    public void maintainSongLoop(AudioSource source)
    {
        if (currentSong % 2 == 0) { source.loop = false; isLooping = false; }
        else { source.loop = true; isLooping = true; }
    }
}
