using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour {
    AIPhase phase;
    SeePlayerCheck targetGet;
    NavMeshAgent agent;
    public float rechargeTime=3.0f;
    float timeLeft=0.0f;
    public int attack = 5;
    public float attackSpeed=30.0f;
    public string attackType = "Melee";
    public GameObject bullet;
    public float bulletVelocity = 100.0f;
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
            agent.angularSpeed = agent.speed * 120;
            if (phase.getPhase().Equals("Attack"))
            {
                if (attackType.Equals("Melee"))
                {
                    agent.speed = attackSpeed;
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
                                if (target.name != "Player")
                                    setTargetToAttacker(target);
                                Debug.Log(targetGet.name + " should attack " + gameObject.name);
                                timeLeft = rechargeTime;
                            }
                        }
                    }
                    else
                    {
                        phase.setDefaultPhase();
                    }
                }
                if(attackType.Equals("Ranged"))
                {
                    GameObject target = GameObject.Find(targetGet.getTarget());
                    if (target)
                    {
                        transform.LookAt(target.transform);
                        if (timeLeft <= 0)
                        {
                            RaycastHit hit;     //Ray Hit data
                            Vector3 rayDirection;
                            rayDirection = target.transform.position - this.transform.position;
                            if (Physics.Raycast(transform.position, rayDirection, out hit))
                            {
                                if (!(hit.collider.name.Contains("Wall")))
                                {
                                    Vector3 bulletStart = transform.position;
                                    GameObject newBullet = (GameObject)Instantiate(bullet, bulletStart, Quaternion.identity);
                                    DestoryAndDamage speedSet = newBullet.GetComponent<DestoryAndDamage>();
                                    speedSet.setSpeed(rayDirection / rayDirection.magnitude * bulletVelocity);
                                    timeLeft = rechargeTime;
                                }
                            }
                            else
                            {
                                phase.setDefaultPhase();
                            }
                        }
                    }

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
