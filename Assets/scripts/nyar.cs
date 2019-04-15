using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class nyar : MonoBehaviour
{
    public RawImage healthBar;
    public float health;
    public GameObject yog;
    public GameObject azathoth;
    GameObject manager;
    manager mng;
    public int moveDisabled = 0;
    public bool activeChar;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("manager");
        mng = manager.GetComponent<manager>();
        health = 100;
        yog = GameObject.Find("yog");
        azathoth = GameObject.Find("azathoth");
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
        {
            mng.lost = true;
        }
    }
    public float moveOne()
    {
        mng.playerStatus = "Nyaralathotep uses Mimicry. You use one of the enemy's moves against him.";
        GameObject enemy = mng.currentEnemy;
        yog yogScript;
        azathoth azaScript;
        yogScript = mng.yog.GetComponent<yog>();
        azaScript = mng.azathoth.GetComponent<azathoth>();
        
        int temp = (int)Random.Range(1, 5);
        if(mng.currentEnemy.name == "yog")
        {
            return yogScript.handleMoves(temp);
        } else
        {
            return azaScript.handleMoves(temp);
        }
        return 0;
    }

    public float moveTwo()
    {
        mng.playerStatus = "Nyaralathotep uses Reveal Self.";
        return 10 * Random.Range(1, 2);
    }

    public float moveThree()
    {
        mng.playerStatus = "Nyaralathotep uses Elevated Siphon. You drain a lot of the enemy's health.";  
        health += 10 * Random.Range(1, 2);
        return 8 * Random.Range(1, 2);

    }

    public float moveFour()
    {
        
        float temp = Random.Range(1, 10);
        if (temp > 7)
        {
            if(mng.currentEnemy.name == "yog")
            {
                yog script = yog.GetComponent<yog>();
                script.stunned = true;
                script.stunCounter = 3;
            } else
            {
                azathoth script = azathoth.GetComponent<azathoth>();
                script.stunned = true;
                script.stunCounter = 3;
            }
            mng.playerStatus = "Nyaralathotep uses Concussed Staff. The enemy is stunned";
        } else
        {
            mng.playerStatus = "Nyaralathotep uses Concussed Staff. The enemy avoids being stunned";
        }
        return 5 * Random.Range(1, 2);
    }

    public float handleMoves(int move)
    {
        if(moveDisabled == move)
        {
            mng.playerStatus = "This move has been disabled";
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
                break;
        }
    }
}
