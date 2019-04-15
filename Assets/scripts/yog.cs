using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class yog : MonoBehaviour
{
    public bool stunned;
    public bool poisoned;
    public float poisonDamage;
    public RawImage healthBar;
    public float health;
    public int moveDisabled = 0;
    float multiplier;
    manager mng;
    public bool activeChar;
    int blockTurns = 0;
    public int stunCounter;
    bool useBlocked;
    
    // Start is called before the first frame update
    void Start()
    {
        stunned = false;
        mng = GameObject.Find("manager").GetComponent<manager>();
        multiplier = 1.3f;
        stunCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(activeChar)
        healthBar.rectTransform.sizeDelta = new Vector2(health, 100);

        if(health > 100)
        {
            health = 100;
        }

        if (health <= 0)
        {
            mng.won = true;
        }
    }

    public float handleMoves(int move)
    {
        if (poisoned)
        {
            health -= poisonDamage;
        }
        if(stunned == false)
        {
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
        } else
        {
            stunCounter--;
            if(stunCounter == 0)
            {
                stunned = false;
            }

            if(blockTurns > 0)
            {
                blockTurns--;
                
            }
        }
        
        return 0;
    }

    public float moveOne()
    {
        
        
        if (blockTurns == 0)
        {
            switch (mng.chosenChar.name)
            {
                case "cthulu":
                    mng.chosenChar.GetComponent<cthulu>().moveDisabled = mng.playerMove;
                    break;
                case "hastur":
                    mng.chosenChar.GetComponent<hustar>().moveDisabled = mng.playerMove;
                    break;
                case "nyar":
                    mng.chosenChar.GetComponent<nyar>().moveDisabled = mng.playerMove;
                    break;
                    
            }
            mng.bossStatus = "Yog Sothoth uses Outer Lock. The last move you used has now been disabled";
            blockTurns = 5;
            mng.blockCounter = 5;
        }

        return 0;
        
    }

    public float moveTwo()
    {
        mng.bossStatus = "Yog Sothoth uses Dissonance. You take moderate damage.";
        return 10 * multiplier;
    }

    public float moveThree()
    {
        mng.bossStatus = "Yog Sothoth uses Omniscience. Its attack power rises significantly";
        multiplier+= multiplier*0.3f;
        health += 10;
        return 0;
    }

    public float moveFour()
    {
        mng.bossStatus = "Yog Sothoth uses Spacial Horror. You take heavy damage";
        return 15 * multiplier;
    }
}
