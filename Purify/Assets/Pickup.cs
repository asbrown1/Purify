using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour {
    public string type;
    public int amount;
    public bool permanent;
    public float rotationSpeed = 5;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Rotate(0, rotationSpeed, 0);
	}
    void OnTriggerEnter(Collider other)
    {
        if(other.transform.root.tag.Equals("Player"))
        {
            if(type.Equals("Health"))
            {
                Health health = other.transform.root.GetComponent<Health>();
                health.addHealth(amount);
                if(permanent)
                {
                    if(PlayerPrefs.HasKey("PickupHealthGain"))
                    {
                        PlayerPrefs.SetInt("PickupHealthGain", PlayerPrefs.GetInt("PickupHealthGain") + amount);
                    }
                }
            }
            if(type.Equals("Mana"))
            {
                Mana mana= other.transform.root.GetComponent<Mana>();
                mana.addMana(amount);
                if (permanent)
                {
                    if (PlayerPrefs.HasKey("PickupManaGain"))
                    {
                        PlayerPrefs.SetInt("PickupManaGain", PlayerPrefs.GetInt("PickupManaGain") + amount);
                    }
                }
            }
            Destroy(this.gameObject);
        }
    }
}
