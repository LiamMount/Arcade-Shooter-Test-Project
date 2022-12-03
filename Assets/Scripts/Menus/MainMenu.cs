using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject savesMenu;
    public GameObject optionsMenu;

    public void BackToMain()
    {
        savesMenu.SetActive(false);
        optionsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void SavesMenu()
    {
        mainMenu.SetActive(false);
        savesMenu.SetActive(true);
    }

    public void LoadMenu()
    {
        //Blank for now, will lead to the file selection
    }

    public void BackToSaves()
    {
        //Blank for now. will lead to the saves menu
    }

    public void OptionsMenu()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
