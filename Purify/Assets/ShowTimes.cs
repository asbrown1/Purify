using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShowTimes : MonoBehaviour {

    Text text;
    int[] times;
    int[] minutes;
    int[] seconds;
    int totalTimes=0;
	// Use this for initialization
	void Start () {
        text = this.GetComponent<Text>();
        times = new int[3];
        minutes = new int[4];
        seconds = new int[4];
        for (int i=0;i<3;i++)
        {
            times[i] = (int)Mathf.Round(PlayerPrefs.GetFloat("LevelMaze"+(i+1)));
            minutes[i] = times[i] / 60;
            seconds[i] = times[i] % 60;
            totalTimes = totalTimes + times[i];
        }
        minutes[3] = totalTimes / 60;
        seconds[3] = totalTimes % 60;
	}
	
	// Update is called once per frame
	void Update () {
        text.text = "You Win!\n"+"Your times \n"+"Level 1: "+minutes[0]+":"+seconds[0]+"\nLevel 2: " + minutes[1] + ":" + seconds[1] + "\nLevel 3: " + minutes[2] + ":" + seconds[2] + "\nTotal Time: "+minutes[3] + ":" + seconds[3];
    }
}
