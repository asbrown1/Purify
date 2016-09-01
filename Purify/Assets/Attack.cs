using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour {
    AIPhase phase;
    SeePlayerCheck targetGet;
    NavMeshAgent agent;
    public int health=100;
    public float rechargeTime=3.0f;
    float timeLeft=0.0f;
    public int attack = 5;
    // Use this for initialization
    void Start () {
        phase = GetComponent<AIPhase>();
        targetGet = GetComponent<SeePlayerCheck>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update() {
        if (this.gameObject.name != "Player")
        {
            if (phase.getPhase().Equals("Attack"))
            {
                Attack targetAttack;
                GameObject target = GameObject.Find(targetGet.getTarget());
                if (target)
                {
                    targetAttack = target.GetComponent<Attack>();
                    if (Vector3.Distance(this.transform.position, target.transform.position) > 3)
                        agent.destination = target.transform.position;
                    else
                    {
                        agent.destination = this.transform.position;
                        if (timeLeft <= 0)
                        {
                            targetAttack.reduceHealth(attack);
                            timeLeft = rechargeTime;
                        }
                    }
                }
                else
                {
                    phase.setPhase("Follow");
                }
            }
        }
        if(timeLeft>0)
        {
            timeLeft = timeLeft-Time.deltaTime;
        }
        if (health <= 0)
            Destroy(this.gameObject);
    }
    public void reduceHealth(int amount)
    {
        health = health - amount;
        Debug.Log(gameObject.name + " now has " + health + " health");
	}
}
