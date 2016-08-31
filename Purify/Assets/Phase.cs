using UnityEngine;
using System.Collections;

public class Phase : MonoBehaviour {

    string currentPhase;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public string getPhase()
    {
        return currentPhase;
    }
    public void setPhase(string toSet)
    {
        currentPhase = toSet;
    }
}
