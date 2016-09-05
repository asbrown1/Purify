using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShowHealth : MonoBehaviour {
    Attack getHealth;
    public RectTransform main;
    public RectTransform healthLeft;
    public RectTransform damageTaken;
	// Use this for initialization
	void Start () {
        getHealth = GetComponent<Attack>();
        /*main = GetComponent<RectTransform>();
        healthLeft = GetComponent<RectTransform>();
        damageTaken = GetComponent<RectTransform>();*/
    }
	
	// Update is called once per frame
	void Update () {
        float width = main.rect.width;
        float height = main.rect.height;
        int health = getHealth.getHealth();
        int maxHealth = getHealth.getMaxHealth();
        float percentHealth = (float)health / maxHealth;
        float percentDamage = (float)(maxHealth - health) / maxHealth;
        float healthOffset = -((width * percentDamage)/width)/2;
        float damageOffset = width;
        healthLeft.sizeDelta = new Vector2(width * percentHealth,height);
        damageTaken.sizeDelta = new Vector2(width * percentDamage,height);
        /*healthLeft.transform.position=new Vector3(healthOffset,0,0);
        damageTaken.transform.position=new Vector3(damageOffset, 0, 0);*/
    }
}
