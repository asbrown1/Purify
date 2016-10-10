using UnityEngine;
using System.Collections;

public class PauseGame : MonoBehaviour
{

    public bool isPaused = false; //check if the game is paused

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            togglePause();
        }
    }

    void togglePause()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}