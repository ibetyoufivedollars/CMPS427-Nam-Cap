using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionsScript : MonoBehaviour {

    public bool fromGame = true;//this is for testing purposes; a separate manager file will take care of these;
    public bool fromMain;

    [SerializeField]
    Slider musicSlider;

    [SerializeField]
    Slider sfxSlider;

    [SerializeField]
    Button backButton;

    [SerializeField]
    AudioSource music;

    [SerializeField]
    AudioSource sfx;

    int nowSong;

    

    // Use this for initialization
    //There will be a variable in AudioManager to keep track of these. When the menu is
    //loaded, the respective sliders are set to what the values were when the menu is entered.
    //This is done to circumvent the sliders and subsequent audio levels being reset
    //to the defaults every time the options menu is loaded.
    void Start () {
        nowSong = GameObject.Find("AudioManager").GetComponent<AudioManager>().getCurrentSong();
        musicSlider.value = GameObject.Find("AudioManager").GetComponent<AudioManager>().musicVolume;
        sfxSlider.value = GameObject.Find("AudioManager").GetComponent<AudioManager>().sfxVolume;
        music.volume = GameObject.Find("AudioManager").GetComponent<AudioManager>().musicVolume;
        sfx.volume = GameObject.Find("AudioManager").GetComponent<AudioManager>().sfxVolume;
        music.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().music[nowSong];
        music.Play();
        musicSlider.onValueChanged.AddListener(delegate { ChangeMusicVolume(); });
        sfxSlider.onValueChanged.AddListener(delegate { ChangeSFXVolume(); });
    }
	
	// Update is called once per frame
	void Update () {
        GameObject.Find("AudioManager").GetComponent<AudioManager>().LoopSong(music, nowSong);
	}

    public void ChangeMusicVolume()
    {
        music.volume = musicSlider.value;
        GameObject.Find("AudioManager").GetComponent<AudioManager>().musicVolume = musicSlider.value;
    }

    public void ChangeSFXVolume()
    {
        sfx.volume = sfxSlider.value;
        GameObject.Find("AudioManager").GetComponent<AudioManager>().sfxVolume = musicSlider.value;
    }

    public void BackClick()
    {
        sfx.clip = GameObject.Find("AudioManager").GetComponent<AudioManager>().sfx[0];
        sfx.Play();
        if (fromGame)
        {
            Debug.Log("Going to the pause menu");
            fromGame = false;//testing purposes
            fromMain = true;//testing purposes
            //SceneManager.LoadScene("PauseMenu");
        }
        else if (fromMain)
        {
            Debug.Log("Going to the main menu");
            fromGame = true;
            fromMain = false;
            //SceneManager.LoadScene("MainMenu");
        }
    }

   
}