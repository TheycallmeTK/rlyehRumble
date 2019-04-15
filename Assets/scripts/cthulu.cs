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
    // Start is called before the first frame update
    void Start()
    {
        health = 100;
        mng = GameObject.Find("manager").GetComponent<manager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(activeChar)
        healthBar.rectTransform.sizeDelta = new Vector2(health, 100);


        if (health > 100)
        {
            health = 100;
        }

        if (health <= 0)
            mng.lost = true;
    }

    public float moveOne()
    {
        mng.playerStatus = "Cthulu uses Jab";
        return 10 * Random.Range(1, 2);
    }
    
    public float moveTwo()
    {
        
        if (isCharging)
        {
            mng.playerStatus = "Cthulu unleashes the full force of Titan Drop";
            isCharging = false;
            return 40 * Random.Range(1, 2);
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
        Instantiate(minion, new Vector3(transform.position.x + 2, 0, 0), Quaternion.identity);
        return 0;
    }

    public float moveFour()
    {
        mng.playerStatus = "Cthulu heals some of its health";
        health += 20;
        return 0;
    }

    public float handleMoves(int move)
    {
        if(moveDisabled == move)
        {
            mng.playerStatus = "That move has been disabled";
            return 0;
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
