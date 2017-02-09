using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{

    public Canvas exitMenu;
    public Button startText;
    public Button optionsText;
    public Button quitText;
    public Canvas optionsMenu;

    // Use this for initialization
    void Start()
    {
        exitMenu = exitMenu.GetComponent<Canvas>();
        startText = startText.GetComponent<Button>();
        optionsText = optionsText.GetComponent<Button>();
        exitMenu.enabled = false;
    }

    public void OptionsPress()
    {
        Debug.Log("Options Menu Pressed!");
        //optionsMenu.enabled = true;
        //startText.enabled = false;
        //optionsText.enabled = false;
        //quitText.enabled = false;
    }

    public void QuitPress()
    {
        Debug.Log("Quit Menu Pressed!");
        exitMenu.enabled = true;
        startText.enabled = false;
        optionsText.enabled = false;
        quitText.enabled = false;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void NoPress()
    {
        exitMenu.enabled = false;
        startText.enabled = true;
        optionsText.enabled = true;
        quitText.enabled = true;
    }

    public void StartGame()
    {
        //SceneManager.LoadScene("");
        Debug.Log("Start Game Pressed!");
    }
}
