using UnityEngine;
using System.Collections;

public class HealBuffV2 : MonoBehaviour {
   //public int accuracy=4;
    public float width=10;
    public int maxLength = 50;
    public int startHealStrength = 20;
    int healStrength;
    public int startBuffStrength = 15;
    int buffStrength;
    public float buffTime = 10;
    public float verticalOffset = 1;
    string currentTarget = "";
    //public float extraDistance = 5;
    //int halfAccuracy;
    Mana mana;
	// Use this for initialization
	void Start () {
        //halfAccuracy = accuracy / 2;
        mana = GetComponent<Mana>();
        buffStrength = startBuffStrength;
        healStrength = startHealStrength;
	}
	
	// Update is called once per frame
	void Update () {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("FriendlyAI");
        float[] angles = new float[targets.Length];
        string[] targetName = new string[targets.Length];
        float minAngle = 180;
        Vector3 playerPosition = new Vector3(this.transform.position.x, this.transform.position.y + verticalOffset, this.transform.position.z);
        Vector3 playerTarget = new Vector3(Camera.main.transform.forward.x,0.5f, Camera.main.transform.forward.z)*10;
        for (int i=0; i < targets.Length; i++)
        {
            targetName[i] = targets[i].gameObject.name;
            Vector3 targetPosition = new Vector3(targets[i].transform.position.x, verticalOffset, targets[i].transform.position.z);
            angles[i] = Vector3.Angle(playerTarget, targetPosition-playerPosition);
            //Debug.Log(angles[i]);
            //Debug.Log("minAngle is " + minAngle);
            if (angles[i] < minAngle)
            {
                RaycastHit hit;
                //Debug.Log("Angle lower than min. Testing raycast");
                if (Physics.Raycast(playerPosition, targetPosition - playerPosition, out hit))
                {
                    Debug.DrawRay(playerPosition, targetPosition - playerPosition,Color.cyan);
                    if (hit.transform.name.Equals(targetName[i]))
                    {
                        minAngle = angles[i];
                        currentTarget = targetName[i];
                    }
                }
            }
        }
        Debug.Log("Closest is " + currentTarget);
        if ((Input.GetMouseButtonDown(0) && mana.canDoSpell("Heal")) || (Input.GetMouseButtonDown(1) && mana.canDoSpell("Buff"))||(Input.GetKeyDown(KeyCode.R)&&mana.canDoSpell("Res"))) //Left click/Right Click
        {
            GameObject target = GameObject.Find(currentTarget);
            if (Input.GetMouseButtonDown(0))
            {
                Health targetHealth = target.GetComponent<Health>();
                if (!(target.GetComponent<AIPhase>().getPhase().Equals("Dead")))
                {
                    targetHealth.getHealth(healStrength);
                    mana.reduceMana("Heal");
                }
            }
            if (Input.GetMouseButtonDown(1))
            {
                Attack targetAttack = target.GetComponent<Attack>();
                if (!(target.GetComponent<AIPhase>().getPhase().Equals("Dead")))
                {
                    targetAttack.getBuff(buffStrength, buffTime);
                    mana.reduceMana("Heal");
                }
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                Health targetHealth = target.GetComponent<Health>();
                AIPhase phase = target.GetComponent<AIPhase>();
                if (phase.getPhase().Equals("Dead"))
                {
                    targetHealth.revive();
                    mana.reduceMana("Res");
                }
            }
        }
    }
    public bool isClosest(string name)
    {
        return (name.Equals(currentTarget));
    }
    public void addBuffStrength(int amount)
    {
        buffStrength = startBuffStrength + amount;
    }
    public void addHealStrength(int amount)
    {
        healStrength = startHealStrength + amount;
    }
}
