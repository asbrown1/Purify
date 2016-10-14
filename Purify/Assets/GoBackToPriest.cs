using UnityEngine;
using System.Collections;

public class GoBackToPriest : MonoBehaviour {

    public float distanceToGoBack = 100.0f;
    public float distanceToAttackAgain = 10.0f;
    public GameObject priest;
    AIPhase phase;
	// Use this for initialization
	void Start () {
        phase = GetComponent<AIPhase>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 toPriest = this.transform.position - priest.transform.position;
        float distanceToPriest = Vector3.Magnitude(toPriest);
        if(distanceToPriest>distanceToGoBack&&!(phase.getPhase().Equals("Dead")))
        {
            Debug.Log("Going back to priest");
            phase.setPhase("FollowNoAttack");
        }
        if(distanceToPriest<distanceToAttackAgain&&phase.getPhase().Equals("FollowNoAttack"))
        {
            phase.setPhase("Follow");
        }
	}
}
