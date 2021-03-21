using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public bool gamePaused = false;
    public GameObject pauseMenu;


    void Update()
    {
        if (Input.GetButtonDown("Cancel")) // Cancel = Esc button
        {
            if (gamePaused == false)
            {
                Time.timeScale = 0; // Stops in game time
                gamePaused = true;
                pauseMenu.SetActive(true);
            }
            else
            {
                pauseMenu.SetActive(false);
                gamePaused = false;
                Time.timeScale = 1; // Starts in game time
            }
        }
    }

    public void UnpauseGame()   // To be called from Menu Resume button
    {
        pauseMenu.SetActive(false);
        gamePaused = false;
        Time.timeScale = 1;
    }

}
