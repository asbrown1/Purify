using UnityEngine;
using System.Collections;
using System;

public class SeePlayerCheck : MonoBehaviour {
    public GameObject[] targets;
    public float fieldOfView;
    public float visionRange;
    AIPhase phase;
    String targetFound="";
	// Use this for initialization
	void Start () {
        phase = GetComponent<AIPhase>();
	}
	
	// Update is called once per frame
	void Update () {
        /*Based on user MattVic's sloution at http://answers.unity3d.com/questions/15735/field-of-view-using-raycasting.html*/
        if (phase.getPhase().Equals("Follow") || phase.getPhase().Equals("Patrol"))
        {
            for (int i = 0; i < targets.Length; i++)       //Checks for all targets
            {
                RaycastHit hit;     //Ray Hit data
                Vector3 rayDirection;   //Direction of ray
                if (targets[i])     //If target still exists
                {
                    rayDirection = targets[i].transform.position - this.transform.position;
                    if (Vector3.Angle(rayDirection, transform.forward) < fieldOfView)   //If character would be in field of view (without taking into account walls)
                    {
                        if (Physics.Raycast(transform.position, rayDirection, out hit, visionRange))    //Casts a ray in the direction of target to check for walls
                        {
                            if (hit.transform.name == targets[i].transform.name)
                            {
                                //Debug.Log(this.gameObject.name + "can see " + hit.transform.name);
                                if (!(phase.getPhase().Equals("Attack")))
                                {
                                    phase.setPhase("Attack");
                                    targetFound = hit.transform.name;
                                }
                            }
                        }
                    }
                    if (Vector3.Magnitude(rayDirection)<2)      //If a target is close enough, it doesn't matter if they are in field of view
                        {
                        //Debug.Log(this.gameObject.name + "can see " + targets[i].transform.name);
                        if (!(phase.getPhase().Equals("Attack")))
                        {
                            phase.setPhase("Attack");
                            targetFound = targets[i].transform.name;
                        }
                    }
                }
            }
        }
	}
    public string getTarget()
    {
        return targetFound;
    }
    public void setTarget(String name)
    {
        targetFound = name;
        if (!(phase.getPhase().Equals("Attack")))
            phase.setPhase("Attack");
        Debug.Log(this.gameObject.name + " is attacking " + targetFound);
    }
}
