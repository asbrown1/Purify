using UnityEngine;
using System.Collections;

public class Patrol : MonoBehaviour {
    public Vector3 [] waypoints;
    int currentWaypoint =0;
    NavMeshAgent agent;
    AIPhase phase;
    public float patrolSpeed = 15.0f;
    // Use this for initialization
    void Start () {
        agent = GetComponent<NavMeshAgent>();
        phase = GetComponent<AIPhase>();
        phase.setPhase("Patrol");
    }
	
	// Update is called once per frame
	void Update () {
        if (phase.getPhase().Equals("Patrol"))
        {
            agent.speed = patrolSpeed;
            Vector3 target = waypoints[currentWaypoint];
            if (target.x == transform.position.x && target.z == transform.position.z)
            {
                if (waypoints.Length > currentWaypoint + 1)
                {
                    currentWaypoint++;
                    Debug.Log(gameObject.name + " entering waypoint " + currentWaypoint);
                }
                else
                    currentWaypoint = 0;
            }
            agent.destination = target;
        }
    }
}
