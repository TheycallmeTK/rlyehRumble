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
    // Start is called before the first frame update
    void Start()
    {
        mng = GameObject.Find("manager").GetComponent<manager>();
        health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (activeChar)
            healthBar.rectTransform.sizeDelta = new Vector2(health, 100);

        
         if(health > 100)
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
        return (10f * Random.Range(1, 2));
    }

    public float moveTwo()
    {
        mng.playerStatus = "Hastur uses Siphon. You take some of the enemy's health";
        health += 10 * Random.Range(1, 2);
        
        return 5 * Random.Range(1, 2);
    }

    public float moveThree()
    {
        mng.playerStatus = "Hastur uses Yellow Sigil. Enemy is poisoned";
        if(mng.currentEnemy.name == "yog")
        {
            yog script = mng.currentEnemy.GetComponent<yog>();
            script.poisoned = true;
            script.poisonDamage = 7 * Random.Range(1, 2);
        } else
        {
            azathoth script = mng.currentEnemy.GetComponent<azathoth>();
            script.poisoned = true;
            script.poisonDamage = 7 * Random.Range(1, 2);
        }
        Debug.Log("poisoned");
        return 0;
    }

    public float moveFour()
    {
        mng.playerStatus = "Hastur uses Refracted Reality. Shield up for five turns.";
        shield = true;
        
        return 0;
    }

    public float handleMoves(int move)
    {
        float temp;
        if (moveDisabled == move)
        {
            mng.playerStatus = "This move has been disabled";
            return 0;
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
