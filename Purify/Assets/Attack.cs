using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour {
    AIPhase phase;
    SeePlayerCheck targetGet;
    NavMeshAgent agent;
    public float rechargeTime=3.0f;
    float timeLeft=0.0f;
    float buffTime = 0.0f;
    public int attack = 5;
    int buffAttack = 0;
    public float attackSpeed=30.0f;
    public string attackType = "Melee";
    public GameObject bullet;
    public float bulletVelocity = 100.0f;
    public bool detailedLog = false;
    // Use this for initialization
    void Start () {
        phase = GetComponent<AIPhase>();
        targetGet = GetComponent<SeePlayerCheck>();
        agent = GetComponent<NavMeshAgent>();
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
            agent.angularSpeed = agent.speed * 120;
            if (phase.getPhase().Equals("Attack"))
            {
                if(detailedLog)
                    Debug.Log(this.gameObject.name + " is in the attack phase");
                if (attackType.Equals("Melee"))
                {
                    if (detailedLog)
                        Debug.Log(this.gameObject.name + " is a melee type");
                    agent.speed = attackSpeed;
                    Health targetHealth;
                    GameObject target = GameObject.Find(targetGet.getTarget());
                    if (target)
                    {
                        if (detailedLog)
                            Debug.Log(this.gameObject.name + " is targetting "+target.gameObject.name);
                        targetHealth = target.GetComponent<Health>();
                        if (Vector3.Distance(this.transform.position, target.transform.position) > 3)
                        {
                            agent.destination = target.transform.position;
                            if (detailedLog)
                                Debug.Log(this.gameObject.name + " is moving towards "+target.gameObject.name);
                        }
                        else
                        {
                            if (detailedLog)
                                Debug.Log(this.gameObject.name + " is close enough to " + target.gameObject.name);
                            agent.destination = this.transform.position;
                            if (timeLeft <= 0)
                            {
                                if (detailedLog)
                                    Debug.Log(this.gameObject.name + " is attacking " + target.gameObject.name);
                                targetHealth.reduceHealth(attack+buffAttack);
                                if (target.name != "Player")
                                    setTargetToAttacker(target);
                                timeLeft = rechargeTime;
                            }
                        }
                    }
                    else
                    {
                        Debug.Log("Taarget not found");
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
                                if (!(hit.collider.tag.Equals("Environment")))
                                {
                                    Vector3 bulletStart = transform.position;
                                    GameObject newBullet = (GameObject)Instantiate(bullet, bulletStart, Quaternion.identity);
                                    DestoryAndDamage speedSet = newBullet.GetComponent<DestoryAndDamage>();
                                    speedSet.setSpeed(rayDirection / rayDirection.magnitude * bulletVelocity);
                                    timeLeft = rechargeTime;
                                }
                                else
                                {
                                    Debug.Log("Target not in range for " + this.gameObject.name + " Resume normal pattern");
                                    phase.setDefaultPhase();
                                }
                            }
                        }
                    }
                    else
                    {
                        Debug.Log("Target not found for " + this.gameObject.name);
                        phase.setDefaultPhase();
                    }

                }
            }
        }
        if(timeLeft>0)
        {
            timeLeft = timeLeft-Time.deltaTime;
        }
        if(buffTime>=0)
        {
            buffTime = buffTime - Time.deltaTime;
        }
        if(buffTime<0)
        {
            buffAttack = 0;
        }
    }
    void setTargetToAttacker(GameObject target)
    {
        SeePlayerCheck swapEnemy = target.GetComponent<SeePlayerCheck>();
        swapEnemy.setTarget(this.gameObject.name);
    }

    public void restoreHealth(int amount)
    {
// HEAD
        //health = health + amount;
        //Debug.Log(gameObject.name + "now has" + health + "health");
//=======
        /*health = health + amount;
        Debug.Log(gameObject.name + "now has" + health + "health");*/
//>>>>>>> refs/remotes/origin/master
    }
    public void getBuff(int strength,float time)
    {
        buffAttack = strength;
        buffTime = time;
    }
}
