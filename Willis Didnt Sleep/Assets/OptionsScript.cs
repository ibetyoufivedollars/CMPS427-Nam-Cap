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
		musicSlider.value = AudioManager.musicVolume;
		sfxSlider.value = AudioManager.sfxVolume;
		music.volume = AudioManager.musicVolume;
		sfx.volume = AudioManager.sfxVolume;
        musicSlider.onValueChanged.AddListener(delegate { ChangeMusicVolume(); });
        sfxSlider.onValueChanged.AddListener(delegate { ChangeSFXVolume(); });
    }
	
	// Update is called once per frame
	void Update () {
	}

    public void ChangeMusicVolume()
    {
        music.volume = musicSlider.value;
        AudioManager.musicVolume = musicSlider.value;
    }

    public void ChangeSFXVolume()
    {
        sfx.volume = sfxSlider.value;
		AudioManager.sfxVolume = musicSlider.value;
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
            SceneManager.LoadScene("TitleScene");
        }
    }

   
}