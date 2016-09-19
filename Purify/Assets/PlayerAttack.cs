using UnityEngine;
using System.Collections;
using System;

public class PlayerAttack : MonoBehaviour {

    LensFlare attackFlare;
    LineRenderer attackLine;
    Light attackLight;
    public float attackLength=1.0f;
    public float flareTime=0.2f;
    public int knockbackStrength = 40;
    public float attackWidth=4.0f;
    public int targetAccuracy = 10;
    float flareStart;
    float flareEnd;
    float middleTime;
    public float peakLightIntensity = 1.5f;
    public float peakFlareIntensity = 2.0f;
    public float lineLength = 10.0f;
    public int attackDamage = 10;
    float timeSinceStart=0.0f;
    bool enabled = false;
    bool damageFactored = false;
    // Use this for initialization
    void Start () {
        attackFlare = GetComponent<LensFlare>();
        attackLine = GetComponent<LineRenderer>();
        attackLight = GetComponent<Light>();
        attackFlare.enabled = false;
        attackLight.enabled = false;
        attackLine.enabled = false;
        flareStart = (attackLength - flareTime) / 2;
        flareEnd = (attackLength + flareTime) / 2;
        middleTime = attackLength / 2;
    }
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKey(KeyCode.Space))
        {
            enabled = true;
        }
        if(enabled)
        {
            doAttack();
            if(timeSinceStart>middleTime&&!damageFactored)
            {
                doDamage();
                damageFactored = true;
            }
        }
        if(timeSinceStart>attackLength)
        {
            enabled = false;
            attackLine.enabled = false;
            timeSinceStart = 0;
            damageFactored = false;
        }
    }
    void doAttack()
    {
        attackFlare.enabled = true;
        attackLight.enabled = true;
        attackLine.enabled = true;
        attackLight.intensity = (float)(-peakLightIntensity*2* (timeSinceStart) * (timeSinceStart - attackLength));
        if (timeSinceStart < flareStart)
        {
            attackFlare.brightness = 0.0f;
            attackLine.SetPosition(1, new Vector3(0, 0.5f, 0));

        }
        else if(timeSinceStart > flareEnd)
        {
            attackFlare.brightness = 0.0f;
        }
        else
        {
            attackFlare.brightness = (float)(-peakFlareIntensity * 200 * (timeSinceStart - flareStart) * (timeSinceStart - flareEnd));
            attackLine.SetPosition(1, new Vector3(0, 0.5f, ((timeSinceStart-flareStart) * lineLength)));
        }
        timeSinceStart = timeSinceStart + Time.deltaTime;
        
        Debug.Log("Attack alive for " + timeSinceStart);
    }

    void doDamage()
    {
        RaycastHit hit;     //Ray Hit data
        Health hitHealth;
        GameObject targetHit;
        Rigidbody targetBody;
        bool hasHit = false;
        int halfTargetAccuracy = targetAccuracy / 2;
        for (int i = -halfTargetAccuracy; i <= halfTargetAccuracy&&!(hasHit); i++)
        {
            Vector3 rayPosition = new Vector3(transform.position.x + (i*attackWidth/targetAccuracy), transform.position.y, transform.position.z);
            Vector3 rayTarget= new Vector3(transform.forward.x + (i * attackWidth / targetAccuracy), transform.forward.y, transform.forward.z);
            if (Physics.Raycast(rayPosition, rayTarget, out hit, lineLength))
            {
                if (!(hit.transform.tag.Equals("Environment") || hit.transform.tag.Equals("Bullet")||hit.transform.tag.Equals("Player")))
                {
                    targetHit = GameObject.Find(hit.transform.name);
                    targetBody = targetHit.GetComponent<Rigidbody>();
                    hitHealth = targetHit.GetComponent<Health>();
                    hitHealth.reduceHealth(attackDamage);
                    hasHit = true;
                    targetBody.AddForce((this.transform.forward/this.transform.forward.magnitude)*knockbackStrength);
                }
            }
        }
    }
}
