using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cthulu : MonoBehaviour
{
    bool isCharging = false;
    public GameObject minion;
    public RawImage healthBar;
    public float health;
    public int moveDisabled = 0;
    public bool activeChar;
    manager mng;
    public Text healthDisplay;
    int lastMove;
    public int combo;
    public float multiplier;
    public bool summoned;
    public int minionCounter;
    public float minionHealth;
    // Start is called before the first frame update
    void Start()
    {
        health = 100;
        mng = GameObject.Find("manager").GetComponent<manager>();
        lastMove = 0;
        combo = 1;
        multiplier = 1.0f;
        minionCounter = 0;
        minionHealth = 30;
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
            mng.lost = true;
    }

    public float moveOne()
    {
        mng.playerStatus = "Cthulu Jabs the enemy";
        
        return 10 * Random.Range(1, 2)*multiplier;
    }
    
    public float moveTwo()
    {
        
        if (isCharging)
        {
            mng.playerStatus = "Cthulu unleashes the full force of Titan Drop";
            isCharging = false;
            switch (combo)
            {
                case 1:
                    return 40 * Random.Range(1, 2);
                case 2:
                    return 50 * Random.Range(1, 2);
                case 3:
                    return 60 * Random.Range(1, 2);
            }
            return 0;
        } else
        {
            mng.playerStatus = "Cthulu charges up Titan Drop";
            isCharging = true;
            return 0;
        }
    }

    public float moveThree()
    {
        mng.playerStatus = "Cthulu summons a minion";
        summoned = true;
        minionCounter = 4;
        minion.transform.position = new Vector3(transform.position.x + 2, 0, 0);
        minionHealth = 40;
        return 0;
    }

    public float moveFour()
    {
        mng.playerStatus = "Cthulu regenerates some health";
        health += 20;
        return 0;
    }

    public float handleMoves(int move)
    {
        
        if (moveDisabled == move)
        {
            mng.playerStatus = "That move has been disabled";
            return 0;
        }
        if (lastMove == move)
        {
            if (combo < 3)
            {
                combo++;
                mng.combo = combo;
            }
        } else
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
            default:
                return 0;
        }
        
    }
}
