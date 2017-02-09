using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class AudioManager : MonoBehaviour {

    [SerializeField]
    public AudioClip[] music = new AudioClip[10];//songs take up 2 spaces due to possible introloops

    [SerializeField]
    public AudioClip[] sfx = new AudioClip[100];

    public AudioSource musicSource;
    public AudioSource sfxSource;

    public static float musicVolume;
    public static float sfxVolume;
    float[] loopValue = new float[] {0, 0, 13.333f, 0, 60.459f};
    bool isLooping = false;
    public int currentSong; //must be an even number!
    public static bool boot = false;
    

    // Use this for initialization
    void Start () {
		musicSource.volume = musicVolume;
		sfxSource.volume = sfxVolume;
        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "Near Final")
        {
            currentSong = 4;//how can we randomize between gameplay songs with flow?
        }
        else if (currentScene == "TitleScene")
        {
            currentSong = 0;
	    if (!boot){
	    musicVolume = .5f;
	    sfxVolume = .5f;
	    boot = true;
	    }
        }
        //when we fight NamCap then currentSong=6
        musicSource.clip = music[currentSong];
        musicSource.Play();
    }
	
	// Update is called once per frame
	void Update () {
        LoopSong(currentSong);
    }

    public void LoopSong(int songId)
    {
        if (musicSource.time == loopValue[songId / 2] && !isLooping)
        {
            musicSource.clip = music[songId + 1];
            currentSong++;
            musicSource.loop = true;
            musicSource.Play();
            isLooping = true;
        }
        maintainSongLoop(musicSource);
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
