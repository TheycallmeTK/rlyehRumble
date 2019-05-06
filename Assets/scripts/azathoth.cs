using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class azathoth : MonoBehaviour
{
    public GameObject minion;
    public float baseDamage;
    public float multiplier;
    public bool stunned;
    public bool poisoned;
    public float poisonDamage;
    public RawImage healthBar;
    public float health;
    public int moveDisabled = 0;
    public bool activeChar;
    manager mng;
    public int stunCounter;
    public Text healthDisplay;
    // Start is called before the first frame update
    void Start()
    {
        health = 100;
        stunned = false;
        poisoned = false;
        multiplier = 1.0f;
        mng = GameObject.Find("manager").GetComponent<manager>();
        stunCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (activeChar)
        {
            healthBar.rectTransform.sizeDelta = new Vector2(health, 100);
            healthDisplay.text = health.ToString();
        }
        if (health > 100)
        {
            health = 100;
        }
        
    }

    public float handleMoves(int move)
    {
        if (poisoned)
        {
            health -= poisonDamage;
            healthBar.rectTransform.sizeDelta = new Vector2(health, 100);
        }
        switch (move)
        {
            case 1:
                return moveOne();
                break;
            case 2:
                return moveTwo();
                break;
            case 3:
                return moveThree();
                break;
            case 4:
                return moveFour();
                break;
        }
        return 0;
    }

    public float moveOne()
    {
        mng.bossStatus = "Azathoth uses radioactivity to deal moderate damage";
        return 10 * Random.Range(1, 2);
    }

    public float moveTwo()
    {
        mng.bossStatus = "Azathoth uses nuclear chaos to deal massive damage";
        return 15 * Random.Range(1, 2);
    }

    public float moveThree()
    {
        mng.bossStatus = "Azathoth heals and grows stronger";
        multiplier = 1.25f;
        mng.enemyHeal = 10;
        return 0;
    }

    public float moveFour()
    {
        mng.bossStatus = "Azathoth uses his finishing move, you have been obliterated";
        switch (mng.chosenChar.name)
        {
            case "cthulu":
                mng.chosenChar.GetComponent<cthulu>().health = 0;
                break;
            case "hastur":
                mng.chosenChar.GetComponent<hustar>().health = 0;
                break;
            case "nyar":
                mng.chosenChar.GetComponent<nyar>().health = 0;
                break;
        }
        return 0;
    }
}
