using UnityEngine;
using System.Collections;

public class AIPhase : MonoBehaviour {

    public string defaultPhase="";
    public string currentPhase;
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
        //Debug.Log("Phase set to " + currentPhase + " for " +gameObject.name);
    }
    public void setDefaultPhase()
    {
        currentPhase = defaultPhase;
    }
}
