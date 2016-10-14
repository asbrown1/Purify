using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShowExperience : MonoBehaviour {

    int experience;
    int level;
    int nextLevelExp;
    Text expText;
    PlayerProgress playerProgress;
    GameObject player;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        playerProgress = player.GetComponent<PlayerProgress>();
        expText = this.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        experience = playerProgress.getExperience();
        level = playerProgress.getExpLevel();
        nextLevelExp = playerProgress.getExpNextLevel();
        expText.text = "Level: " + (level+1) + "\nExperience: " + experience + "/" + nextLevelExp;
	}
}
