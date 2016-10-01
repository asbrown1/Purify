using UnityEngine;
using System.Collections;

public class Keys : MonoBehaviour {

    string[] keyNames;
    int keyHave = 0;
	// Use this for initialization
	void Start () {
        int numberOfKeys = GameObject.FindGameObjectsWithTag("Key").Length;
        keyNames = new string[numberOfKeys];
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void addKey(string name)
    {
        keyNames[keyHave] = name;
        keyHave++;
    }
    public bool hasKey(string name)
    {
        for(int i=0;i<keyHave;i++)
        {
            if(keyNames[i].Equals(name))
            {
                return true;
            }
        }
        return false;
    }
}
