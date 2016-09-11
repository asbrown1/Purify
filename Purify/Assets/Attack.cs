using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour {
    AIPhase phase;
    SeePlayerCheck targetGet;
    NavMeshAgent agent;
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
                Health targetHealth;
                GameObject target = GameObject.Find(targetGet.getTarget());
                if (target)
                {
                    targetHealth = target.GetComponent<Health>();
                    if (Vector3.Distance(this.transform.position, target.transform.position) > 3)
                        agent.destination = target.transform.position;
                    else
                    {
                        agent.destination = this.transform.position;
                        if (timeLeft <= 0)
                        {
                            Debug.Log(gameObject.name);
                            targetHealth.reduceHealth(attack);
                            if(target.name!="Player")
                                setTargetToAttacker(target);
                            Debug.Log(targetGet.name + " should attack " + gameObject.name);
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
    }
    void setTargetToAttacker(GameObject target)
    {
        SeePlayerCheck swapEnemy = target.GetComponent<SeePlayerCheck>();
        swapEnemy.setTarget(this.gameObject.name);
    }
}
