using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    GameManager gm;

    public GameObject pauseMenuUI;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (!gm.isCutscene)
        {
            GetPauseInput();
        }
        if (gm.isPaused && !gm.isCutscene)
        {
            MenuNavigation();
        }
    }

    // Pause Logic

    public void GetPauseInput()
    {
        if (Input.GetButtonDown("Start") && !gm.isPaused && !gm.isCutscene)
        {
            Pause();
        }
        else if (Input.GetButtonDown("Start") && gm.isPaused)
        {
            Resume();
        }
    }

    public void Pause()
    {
        Time.timeScale = 0f;

        gm.isPaused = true;
        pauseMenuUI.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1f;

        gm.isPaused = false;
        pauseMenuUI.SetActive(false);
    }

    public void MenuNavigation()
    {
        // Animate whatever
    }
}
