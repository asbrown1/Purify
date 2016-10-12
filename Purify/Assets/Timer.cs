using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour {
    string currentLevel;
    float time;
    // Use this for initialization
    void Start () {
        currentLevel = SceneManager.GetActiveScene().name;
        time = 0;
        Debug.Log("Level" + currentLevel);
    }
	
	// Update is called once per frame
	void Update () {

        time = time + Time.deltaTime;
    }
    public void recordTime()
    {
        PlayerPrefs.SetFloat("Level" + currentLevel, time);
        PlayerPrefs.Save();
        Debug.Log("Set Level" + currentLevel + " to " + time);
    }
}
