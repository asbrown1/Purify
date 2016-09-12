using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour {
    AIPhase phase;
    SeePlayerCheck targetGet;
    NavMeshAgent agent;
    public int maxHealth=100;
    int health;
    public float rechargeTime=3.0f;
    float timeLeft=0.0f;
    public int attack = 5;
    // Use this for initialization
    void Start () {
        phase = GetComponent<AIPhase>();
        targetGet = GetComponent<SeePlayerCheck>();
        agent = GetComponent<NavMeshAgent>();
        health = maxHealth;
    }

    // Update is called once per frame
    void Update() {

        //healing the ai, may not need to be there
        if (Input.mousePosition == this.transform.position) //this is wrong tbh fam
        {
            restoreHealth(attack);
        }


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

    public int getHealth()
    {
        return health;
    }

    public int getMaxHealth()
    {
        return maxHealth;
    }

    public void restoreHealth(int amount)
    {
        health = health + amount;
        Debug.Log(gameObject.name + "now has" + health + "health");
    }
}
