using UnityEngine;
using System.Collections;
using System;

public class PlayerAttack : MonoBehaviour {

    LensFlare attackFlare;
    LineRenderer attackLine;
    Light attackLight;
    public float attackLength=1.0f;
    public float flareTime=0.2f;
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
            attackLine.SetPosition(1, new Vector3(0, 0.5f, ((timeSinceStart-flareStart) * lineLength*5)));
        }
        timeSinceStart = timeSinceStart + Time.deltaTime;
        
        Debug.Log("Attack alive for " + timeSinceStart);
    }

    void doDamage()
    {
        RaycastHit hit;     //Ray Hit data
        Health hitHealth;
        GameObject targetHit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, lineLength))
        {
            if (!(hit.transform.name.Contains("Wall") || hit.transform.name.Contains("Terrain") || hit.transform.name.Contains("Bullet")))
            {
                targetHit = GameObject.Find(hit.transform.name);
                hitHealth = targetHit.GetComponent<Health>();
                hitHealth.reduceHealth(attackDamage);
            }
        }
    }
}
