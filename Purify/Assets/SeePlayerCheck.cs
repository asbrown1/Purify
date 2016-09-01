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
            for (int i = 0; i < targets.Length; i++)
            {
                RaycastHit hit;
                Vector3 rayDirection;
                if (targets[i])
                {
                    rayDirection = targets[i].transform.position - this.transform.position;
                    if (Vector3.Angle(rayDirection, transform.forward) < fieldOfView)
                    {
                        if (Physics.Raycast(transform.position, rayDirection, out hit, visionRange))
                        {
                            if (hit.transform.name == targets[i].transform.name)
                            {
                                Debug.Log(this.gameObject.name + "can see " + hit.transform.name);
                                if (!(phase.getPhase().Equals("Attack")))
                                {
                                    phase.setPhase("Attack");
                                    targetFound = hit.transform.name;
                                }
                            }
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
}
