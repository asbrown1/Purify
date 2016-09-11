using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {

    public GameObject player;
    public float distance = 5.0f;
    public float angle = 30.0f;
    public float force=0.2f;
    public float followSpeed = 15.0f;
    NavMeshAgent agent;
    AIPhase phase;
    // Use this for initialization
    void Start () {
        agent = GetComponent<NavMeshAgent>();
        phase = GetComponent<AIPhase>();
        phase.setPhase("Follow");
    }
	
	// Update is called once per frame
	void Update () {
        if (phase.getPhase().Contains("Follow"))
        {
            if(phase.getPhase().Equals("Follow"))
                agent.speed = followSpeed;
            Vector3 playerPosition = player.transform.position;
            Vector3 playerRotation = player.transform.rotation.eulerAngles;
            float zValue = distance * Mathf.Cos((playerRotation.y + angle) * Mathf.Deg2Rad);
            float xValue = distance * Mathf.Sin((playerRotation.y + angle) * Mathf.Deg2Rad);
            Vector3 playerOffset = new Vector3(playerPosition.x + xValue, playerPosition.y, playerPosition.z + zValue); //Target position from player
            agent.destination = playerOffset;
        }
	}
}
