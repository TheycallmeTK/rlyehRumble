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
        }
        
    }

    public float handleMoves(int move)
    {
        if (poisoned)
        {
            health -= poisonDamage;
            healthBar.GetComponent<Rect>().size.Set(health, 5);
        }
        switch (move)
        {
            case 1:
                
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
        }
        return 0;
    }

    public float moveOne()
    {
        return 10 * Random.Range(1, 2);
    }

    public float moveTwo()
    {
        return 15 * Random.Range(1, 2);
    }

    public float moveThree()
    {
        multiplier = 1.25f;
        return 0;
    }

    public float moveFour()
    {
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
