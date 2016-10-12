using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShowTimes : MonoBehaviour {

    Text text;
    int[] times;
    int totalTimes=0;
	// Use this for initialization
	void Start () {
        text = this.GetComponent<Text>();
        times = new int[3];
        for(int i=0;i<3;i++)
        {
            times[i] = (int)Mathf.Round(PlayerPrefs.GetFloat("LevelMaze"+i+1));
            totalTimes = totalTimes + times[i];
        }
	}
	
	// Update is called once per frame
	void Update () {
        text.text = "You Win!\n"+"Your times \n"+"Level 1: "+times[0]+"\nLevel 2: "+times[1]+"\nLevel 3: "+times[2]+"\nTotal Time: "+totalTimes;
    }
}
