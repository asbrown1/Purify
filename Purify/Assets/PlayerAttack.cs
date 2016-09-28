using UnityEngine;
using System.Collections;
using System;

public class PlayerAttack : MonoBehaviour {

    LensFlare attackFlare;
    LineRenderer attackLine;
    Light attackLight;
    Move movement;
    public Camera mainCam;
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
    Mana mana;
    // Use this for initialization
    void Start () {
        attackFlare = GetComponent<LensFlare>();
        attackLine = GetComponent<LineRenderer>();
        attackLight = GetComponent<Light>();
        movement = GetComponent<Move>();
        mana = GetComponent<Mana>();
        attackFlare.enabled = false;
        attackLight.enabled = false;
        attackLine.enabled = false;
        flareStart = (attackLength - flareTime) / 2;
        flareEnd = (attackLength + flareTime) / 2;
        middleTime = attackLength / 2;
    }
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKey(KeyCode.Space)&&mana.canDoSpell("Attack"))
        {
            enabled = true;
            mana.reduceMana("Attack");
        }
        if(enabled)
        {
            movement.faceCamera();
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
            attackLine.SetPosition(1, new Vector3(0, 0.5f, ((timeSinceStart - flareStart) * lineLength)));
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
        Vector3 playerRotation = this.transform.rotation.eulerAngles;
        bool hasHit = false;
        int halfTargetAccuracy = targetAccuracy / 2;
        for (int i = -halfTargetAccuracy; i <= halfTargetAccuracy&&!(hasHit); i++)
        {
            float xValue = Mathf.Sin((float)(playerRotation.y+i*attackWidth/targetAccuracy) * Mathf.Deg2Rad) *lineLength;
            float zValue = Mathf.Cos((float)(playerRotation.y+i*attackWidth/targetAccuracy) * Mathf.Deg2Rad) * lineLength;
            Vector3 rayPosition = new Vector3(transform.position.x, 0.5f, transform.position.z);
            Vector3 rayTarget= new Vector3(xValue, 0.5f, zValue);
            if (Physics.Raycast(rayPosition, rayTarget, out hit, lineLength))
            {
                Debug.DrawRay(rayPosition, rayTarget, Color.green, 5,false);
                targetHit = GameObject.Find(hit.transform.name).transform.root.gameObject;
                if (!(targetHit.transform.tag.Equals("Environment") || targetHit.transform.tag.Equals("Bullet")||targetHit.transform.tag.Equals("Player")))
                {
                    Debug.Log(targetHit.transform.tag);
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
