using UnityEngine;
using System.Collections;

public class HealBuff : MonoBehaviour {
    public int accuracy=4;
    public float width=10;
    public int maxLength = 50;
    public int healStrength = 10;
    public int buffStrength = 10;
    public float buffTime = 10;
    public float verticalOffset = 1;
    public float extraDistance = 5;
    int halfAccuracy;
    Mana mana;
	// Use this for initialization
	void Start () {
        halfAccuracy = accuracy / 2;
        mana = GetComponent<Mana>();
	}
	
	// Update is called once per frame
	void Update () {
       /* if (!mana.canDoSpell("Heal"))
            Debug.Log("Cannot heal");
        if (mana.canDoSpell("Heal"))
            Debug.Log("Can heal");*/
        if ((Input.GetMouseButtonDown(0)&&mana.canDoSpell("Heal"))||(Input.GetMouseButtonDown(1)&&mana.canDoSpell("Buff"))) //Left click/Right Click
        {
            Debug.Log("Entering buff/heal");
            GameObject [] targets = GameObject.FindGameObjectsWithTag("FriendlyAI");
            float[] targetDistances = new float [targets.Length];
            string[] targetName = new string[targets.Length];
            Vector3[,,] targetLines = new Vector3[targets.Length,accuracy,accuracy];
            Vector3[,,] targetAngles = new Vector3[targets.Length, accuracy, accuracy];
            Vector3 mousePos = Input.mousePosition;
            Vector3 playerPosition = new Vector3(this.transform.position.x, this.transform.position.y+verticalOffset, this.transform.position.z);
            RaycastHit hit;
            bool healed = false;
            for(int i=0;i<targets.Length&&!(healed);i++)      //Get distances to targets, then convert a position in world state equal to their distances away and ending at the mouse click.
            {
                targetName[i] = targets[i].gameObject.name;
                targetDistances[i] = Vector3.Magnitude(playerPosition - targets[i].transform.position)+extraDistance;
                if(targetDistances[i]>maxLength)
                {
                    targetDistances[i] = maxLength;
                }
                //targetLines[i,halfAccuracy] = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, targetDistances[i]));
                for (int j = -halfAccuracy; j < halfAccuracy && !(healed); j++)
                {
                    
                    for(int k=-halfAccuracy;k<halfAccuracy && !(healed); k++)
                    {
                        targetLines[i, (j + halfAccuracy), (k+halfAccuracy)] = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x + j * width, mousePos.y + k * width, targetDistances[i]));
                        if (Physics.Raycast(playerPosition, targetLines[i, (j + halfAccuracy), k + halfAccuracy]-this.transform.position, out hit, targetDistances[i]))
                        {
                            Debug.Log(hit.transform.name);
                            if (hit.transform.name.Equals(targetName[i]))
                            {
                                if (Input.GetMouseButtonDown(0))
                                {
                                    //Heal animation goes here
                                    Health targetHealth = targets[i].GetComponent<Health>();
                                    targetHealth.getHealth(healStrength);
                                    Debug.Log(targetName[i] + " was healed");
                                    healed = true;
                                    mana.reduceMana("Heal");
                                }
                                if(Input.GetMouseButtonDown(1))
                                {
                                    //Buff animation goes here
                                    Attack targetAttack = targets[i].GetComponent<Attack>();
                                    targetAttack.getBuff(buffStrength, buffTime);
                                    healed = true;
                                    mana.reduceMana("Buff");
                                }
                            }
                            Debug.DrawRay(playerPosition, targetLines[i, (j + halfAccuracy), k + halfAccuracy]-this.transform.position, Color.red, 3);
                        }

                    }
                }
            }
            healed = false;
        }
	}
}
