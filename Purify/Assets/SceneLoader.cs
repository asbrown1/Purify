﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

    public void loadLastLevel()
    {
        string lastLevel = PlayerPrefs.GetString("LastLevel");
        SceneManager.LoadScene(lastLevel);
    }

    public void loadLevel1()
    {
        SceneManager.LoadScene("Maze1");
    }

    public void loadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
