using UnityEngine;
using System.Collections;
using System;

public class SeePlayerCheck : MonoBehaviour {
    GameObject[] targets;
    GameObject[] targets2;
    GameObject[] totalTargets;
    public float fieldOfView;
    public float visionRange;
    public Boolean detailedLog = false;
    AIPhase phase;
    String targetFound="";
    String targetTag;
    String targetTag2;
	// Use this for initialization
	void Start () {
        phase = GetComponent<AIPhase>();
        if (this.gameObject.tag.Equals("Enemy"))
        {
            targetTag = "FriendlyAI";
            targetTag2 = "Player";
        }
        if (this.gameObject.tag.Equals("FriendlyAI"))
        {
            targetTag = "Enemy";
            targetTag2 = "Untagged";
        }
        targets = GameObject.FindGameObjectsWithTag(targetTag);
        targets2= GameObject.FindGameObjectsWithTag(targetTag2);
        totalTargets = new GameObject[targets.Length + targets2.Length];
        targets.CopyTo(totalTargets, 0);
        targets2.CopyTo(totalTargets, targets.Length);

    }
	
	// Update is called once per frame
	void Update () {
        /*Based on user MattVic's sloution at http://answers.unity3d.com/questions/15735/field-of-view-using-raycasting.html*/
        if (phase.getPhase().Equals("Follow") || phase.getPhase().Equals("Patrol"))
        {
            for (int i = 0; i < totalTargets.Length; i++)       //Checks for all targets
            {
                RaycastHit hit;     //Ray Hit data
                Vector3 rayDirection;   //Direction of ray
                Vector3 rayStart = new Vector3(transform.position.x, 1, transform.position.z);
                if (totalTargets[i])     //If target still exists
                {
                    Vector3 targetPosition = new Vector3(totalTargets[i].transform.position.x, 1, totalTargets[i].transform.position.z);
                    if (detailedLog)
                        Debug.Log(this.gameObject.name + " is checking " + totalTargets[i].name);
                    rayDirection = targetPosition - rayStart;
                    if (Vector3.Angle(rayDirection, transform.forward) < fieldOfView)   //If character would be in field of view (without taking into account walls)
                    {
                        if (detailedLog)
                            Debug.Log(totalTargets[i].name + " is in field of view of " + this.gameObject.name);
                        Debug.DrawRay(rayStart, rayDirection, Color.blue);
                        if (Physics.Raycast(rayStart, rayDirection, out hit, visionRange))    //Casts a ray in the direction of target to check for walls
                        {
                            if (hit.transform.root.transform.name == totalTargets[i].transform.name)
                            {
                                if(detailedLog)
                                    Debug.Log(totalTargets[i].name + " can see " + this.gameObject.name);
                                if (!(phase.getPhase().Equals("Attack")))
                                {
                                    phase.setPhase("Attack");
                                    targetFound = hit.transform.root.transform.name;
                                    //Debug.Log("Found " + targetFound);
                                }
                            }
                            else
                            {
                                if (detailedLog)
                                    Debug.Log("A wall is in the way, or character out of vision range");
                            }
                        }
                    }
                    if (Vector3.Magnitude(rayDirection)<2)      //If a target is close enough, it doesn't matter if they are in field of view
                        {
                        //Debug.Log(this.gameObject.name + "can see " + targets[i].transform.name);
                        if (!(phase.getPhase().Equals("Attack")))
                        {
                            phase.setPhase("Attack");
                            targetFound = totalTargets[i].transform.root.transform.name;
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
