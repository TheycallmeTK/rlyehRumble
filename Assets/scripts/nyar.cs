using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class nyar : MonoBehaviour
{
    public RawImage healthBar;
    public float health;
    public Text healthDisplay;
    public GameObject yog;
    public GameObject azathoth;
    GameObject manager;
    manager mng;
    public int moveDisabled = 0;
    public bool activeChar;
    public int combo;
    int lastMove;
    public float multiplier;

    //audio
    public AudioSource mimicry;
    public AudioSource revealSelf;
    public AudioSource elevatedSiphon;
    public AudioSource concussedStaff;

    // Start is called before the first frame update
    void Start()
    {
        multiplier = 1.0f;
        manager = GameObject.Find("manager");
        mng = manager.GetComponent<manager>();
        health = 100;
        yog = GameObject.Find("yog");
        azathoth = GameObject.Find("azathoth");
        
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
        mng.playerStatus = "Nyaralathotep uses Mimicry. He uses one of the enemy's moves against him.";
        mimicry.Play();
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
        mng.playerStatus = "Nyaralathotep uses Reveal Self. The enemy takes moderate damage.";
        revealSelf.Play();
        return 13 * Random.Range(1, 2)*multiplier;
    }

    public float moveThree()
    {
        mng.playerStatus = "Nyaralathotep uses Elevated Siphon. You drain a lot of the enemy's health.";
        elevatedSiphon.Play();
        health += 10 * Random.Range(1, 2)*multiplier;
        return 13 * Random.Range(1, 2)*multiplier;

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
            concussedStaff.Play();
        } else
        {
            mng.playerStatus = "Nyaralathotep uses Concussed Staff. The enemy avoids being stunned";
            concussedStaff.Play();
        }
        return 5 * Random.Range(1, 2)*multiplier;
    }

    public float handleMoves(int move)
    {
        if(moveDisabled == move)
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
            combo = 2;
            mng.combo = combo;
        }
        lastMove = move;
        if (combo == 2)
        {
            multiplier = 1.2f;
        }
        if (combo == 3)
        {
            multiplier = 1.4f;
        }
        if(combo == 1)
        {
            multiplier = 1.0f;
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
