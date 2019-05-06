using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hustar : MonoBehaviour
{
    public RawImage healthBar;
    public float health;
    manager mng;
    public bool shield;
    public int moveDisabled = 0;
    public bool activeChar;
    public Text healthDisplay;
    public int combo;
    public Animation anim;
    public float multiplier;
    int shieldCounter;
   
    int lastMove;
    // Start is called before the first frame update
    void Start()
    {
        shieldCounter = 0;
        multiplier = 1.0f;
        mng = GameObject.Find("manager").GetComponent<manager>();
        health = 100;
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
        if (health <= 0)
        {
            mng.lost = true;
        }
        
    }
    public float moveOne()
    {
        mng.playerStatus = "Hastur uses Flame";
        return (10f * Random.Range(1, 2))*multiplier;
    }

    public float moveTwo()
    {
        mng.playerStatus = "Hastur uses Siphon. He takes some of the enemy's health";
        health += 10 * Random.Range(1, 2)*multiplier;
        
        return 5 * Random.Range(1, 2)*multiplier;
    }

    public float moveThree()
    {
        mng.playerStatus = "Hastur uses Yellow Sigil to poison the enemy";
        if(mng.currentEnemy.name == "yog")
        {
            yog script = mng.currentEnemy.GetComponent<yog>();
            script.poisoned = true;
            script.poisonDamage = 7 * Random.Range(1, 2)*multiplier;
        } else
        {
            azathoth script = mng.currentEnemy.GetComponent<azathoth>();
            script.poisoned = true;
            script.poisonDamage = 7 * Random.Range(1, 2)*multiplier;
        }
        Debug.Log("poisoned");
        return 0;
    }

    public float moveFour()
    {
        mng.playerStatus = "Hastur uses Refracted Reality to guard himself. Shield up for five turns.";
        shield = true;
        shieldCounter = 5;
        return 0;
    }

    public float handleMoves(int move)
    {
        if (shield)
        {
            shieldCounter--;
        }
        if (shieldCounter == 0)
        {
            shield = false;
        }
        float temp;
        if (moveDisabled == move)
        {
            mng.playerStatus = "This move has been disabled";
            return 0;
        }
        if (lastMove == move)
        {
            if (combo < 3)
            {
                combo++;
                mng.combo = combo;
            }
        }
        else
        {
            combo = 1;
            mng.combo = combo;
        }
        lastMove = move;
        switch (combo)
        {
            case 1:
                multiplier = 1.0f;
                break;
            case 2:
                multiplier = 1.2f;
                break;
            case 3:
                multiplier = 1.4f;
                break;
        }
        switch (move)
        {
            case 1:
                temp = moveOne();
                return temp;
                break;
            case 2:
                temp = moveTwo();
                return temp;
                break;
            case 3:
                temp = moveThree();
                return temp;
                break;
            case 4:
                temp = moveFour();
                return temp;
                break;
            default:
                return 0;
                break;
        }
    }
}
