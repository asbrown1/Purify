using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour {
    AIPhase phase;
    SeePlayerCheck targetGet;
    NavMeshAgent agent;
    bool particlesPlaying = false;
    public float rechargeTime=3.0f;
    float timeLeft=0.0f;
    float buffTime = 0.0f;
    public int startAttack = 5;
    int attack;
    int buffAttack = 0;
    public float attackSpeed=30.0f;
    public string attackType = "Melee";
    public GameObject bullet;
    public float bulletVelocity = 100.0f;
    public bool detailedLog = false;
    float particleTime = 0f;
    ParticleSystem particles;
    // Use this for initialization
    void Start () {
        attack = startAttack;
        phase = GetComponent<AIPhase>();
        targetGet = GetComponent<SeePlayerCheck>();
        agent = GetComponent<NavMeshAgent>();
        if(GetComponent<ParticleSystem>())
            particles = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.name != "Player")
        {
            agent.angularSpeed = agent.speed * 120;
            if (phase.getPhase().Equals("Attack")|| phase.getPhase().Equals("AttackAI"))
            {
                if (detailedLog)
                    Debug.Log(this.gameObject.name + " is in the attack phase");
                if (attackType.Equals("Melee"))
                {
                    if (detailedLog)
                        Debug.Log(this.gameObject.name + " is a melee type");
                    agent.speed = attackSpeed;
                    Health targetHealth;
                    GameObject target = GameObject.Find(targetGet.getTarget());
                    if (target&&!(target.GetComponent<AIPhase>().getPhase().Equals("Dead")))
                    {
                        if (detailedLog)
                            Debug.Log(this.gameObject.name + " is targetting " + target.gameObject.name);
                        targetHealth = target.GetComponent<Health>();
                        if (Vector3.Distance(this.transform.position, target.transform.position) > 5)
                        {
                            agent.destination = target.transform.position;
                            if (detailedLog)
                                Debug.Log(this.gameObject.name + " is moving towards " + target.gameObject.name);
                        }
                        else
                        {
                            if (detailedLog)
                                Debug.Log(this.gameObject.name + " is close enough to " + target.gameObject.name);
                            agent.destination = this.transform.position;
                            if (timeLeft <= 0 && !(this.phase.getPhase().Equals("Dead")))
                            {
                                if (detailedLog)
                                    Debug.Log(this.gameObject.name + " is attacking " + target.gameObject.name);
                                //Attack animation goes here
                                if(this.gameObject.tag.Equals("Enemy")||this.gameObject.tag.Equals("FriendlyAI"))
                                {
                                    Animator anim = this.GetComponent<Animator>();
                                    anim.SetTrigger("Attack");
                                }
                                if (this.gameObject.tag.Equals("Boss"))
                                {
                                    Animation anim = this.transform.GetChild(1).GetComponent<Animation>();
                                    int random = (int)Random.value * 2;
                                    if (random == 1)
                                        anim.Play("attack01");
                                    else
                                        anim.Play("attack02");
                                }
                                targetHealth.reduceHealth(attack + buffAttack,Vector3.Normalize(this.transform.position-target.transform.position),true);
                                if (target.name != "Player")
                                    setTargetToAttacker(target);
                                timeLeft = rechargeTime;
                            }
                        }
                    }
                    else
                    {
                        Debug.Log("Target not found");
                        phase.setDefaultPhase();
                    }
                }
                if (attackType.Equals("Ranged"))
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
                                if (!(hit.collider.gameObject.transform.root.gameObject.tag.Equals("Environment")||hit.collider.gameObject.transform.root.GetComponent<AIPhase>().getPhase().Equals("Dead")))
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
        if (timeLeft > 0)
        {
            timeLeft = timeLeft - Time.deltaTime;
        }
        if (buffTime >= 0)
        {
            buffTime = buffTime - Time.deltaTime;
        }
        if (buffTime < 0)
        {
            buffAttack = 0;
        }
        if (particles)
        {
            if (particleTime > 0)
            {
                if (!(particlesPlaying))
                {
                    particles.Play();
                    particlesPlaying = true;
                    this.GetComponent<Health>().disableParticles();
                }
                particleTime = particleTime - Time.deltaTime;
            }
            else if(particles.startColor==Color.red)
            {
                particles.Stop();
                particlesPlaying = false;
            }
        }
    }
        void setTargetToAttacker(GameObject target)
        {
        SeePlayerCheck swapEnemy = target.GetComponent<SeePlayerCheck>();
        swapEnemy.setTarget(this.gameObject.name);
        }

    public void getBuff(int strength,float time)
    {
        ParticleSystem particles = GetComponent<ParticleSystem>();
        particles.startColor = Color.red;
        particleTime = time;
        buffAttack = strength;
        buffTime = time;
    }
    public void gainAttack(int amount)
    {
        attack = startAttack + amount;
    }
    public void disableParticles()  //Doesn't actually disable particles. More so if particles switch to green for heals and the particlesPlaying is never set to false
    {
        particlesPlaying = false;
    }
}
