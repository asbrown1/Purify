using UnityEngine;
using System.Collections;

public class Patrol : MonoBehaviour {
    public Vector3 [] waypoints;
    int currentWaypoint =0;
    NavMeshAgent agent;
    // Use this for initialization
    void Start () {
        agent = GetComponent<NavMeshAgent>();
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 target = waypoints[currentWaypoint];
        if(target.x==transform.position.x&& target.z == transform.position.z)
        {
            if (waypoints.Length > currentWaypoint + 1)
            {
                currentWaypoint++;
            }
            else
                currentWaypoint = 0;
        }
        agent.destination = target;
    }
}
