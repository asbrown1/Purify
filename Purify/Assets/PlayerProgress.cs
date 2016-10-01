using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerProgress : MonoBehaviour {
    string currentLevel;

	// Use this for initialization
	void Start () {
        currentLevel = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString("LastLevel", currentLevel);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
