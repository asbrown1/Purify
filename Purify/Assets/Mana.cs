using UnityEngine;
using System.Collections;

public class Mana : MonoBehaviour {

    public int maxMana=100;
    public int manaCostHeal=20;
    public int manaCostBuff=20;
    public int manaCostRes = 100;
    public int manaCostAttack = 10;
    public float healCoolDown = 5;
    public float buffCoolDown = 10;
    public float attackCoolDown = 2;
    public int resLeft = 1;
    public float manaGainRate=4f;
    int mana;
    float healLeft=0;
    float buffLeft=0;
    float attackLeft=0;
    float manaTimePerGain;
    float manaTimeLeft;

	// Use this for initialization
	void Start () {
        mana = maxMana;
        manaTimePerGain = 1 / manaGainRate;
        manaTimeLeft = manaTimePerGain;
    }
	
	// Update is called once per frame
	void Update () {
	    if(healLeft>0)
        {
            healLeft = healLeft - Time.deltaTime;
        }
        if (attackLeft > 0)
        {
            attackLeft = attackLeft - Time.deltaTime;
        }
        if (buffLeft > 0)
        {
            buffLeft = buffLeft - Time.deltaTime;
        }
        if(mana<maxMana)
        {
            if (manaTimeLeft > 0)
            {
                manaTimeLeft = manaTimeLeft - Time.deltaTime;
            }
            else
            {
                mana++;
                manaTimeLeft = manaTimePerGain;
            }
        }
    }

    public void reduceMana(string type)
    {
        if (type.Equals("Attack"))
        {
            attackLeft = attackCoolDown;
            mana = mana - manaCostAttack;
        }
        else if(type.Equals("Heal"))
        {
            healLeft = healCoolDown;
            mana = mana - manaCostHeal;
        }
        else if(type.Equals("Buff"))
        {
            buffLeft = buffCoolDown;
            mana = mana - manaCostBuff;
        }
        else if(type.Equals("Res"))
        {
            resLeft--;
            mana = mana - manaCostRes;
        }
    }

    public bool canDoSpell(string type)
    {
        if (type.Equals("Attack"))
        {
            if (mana > manaCostAttack && attackLeft <= 0)
                return true;
            else
                return false;
        }
        else if (type.Equals("Heal"))
        {
            if (mana > manaCostHeal && healLeft <= 0)
                return true;
            else
                return false;
        }
        else if (type.Equals("Buff"))
        {
            if (mana > manaCostBuff && buffLeft <= 0)
                return true;
            else
                return false;
        }
        else if (type.Equals("Res"))
        {
            if (mana > manaCostRes && resLeft > 0)
                return true;
            else
                return false;
        }
        else
            return false;
    }
    public int getMana()
    {
        return mana;
    }
    public int getMaxMana()
    {
        return maxMana;
    }
}
