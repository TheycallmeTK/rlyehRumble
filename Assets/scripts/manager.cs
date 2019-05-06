using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class manager : MonoBehaviour
{
    public Canvas title;
    public Canvas info;
    public RawImage cthuluThumb;
    public RawImage hasturThumb;
    public RawImage nyarThumb;
    public Canvas characterSelect;
    public RawImage transit;

    public GameObject cthuluSprite;
    public GameObject cthulu;
    public GameObject hustar;
    public GameObject nyar;
    public GameObject yog;
    public GameObject azathoth;
    public GameObject chosenChar;
    public GameObject currentEnemy;

    public GameObject indi1;
    public GameObject indi2;
    public GameObject indi3;
    public GameObject indi4;

    int lastMove;

    yog yogScript;
    azathoth azathothScript;
    float damage;

    public Text move1;
    public Text move2;
    public Text move3;
    public Text move4;

    public int playerMove;
    public int enemyMove;

    public GameObject statusBox;
    public Text statusText;

    public GameObject moveBox;
    public string playerStatus;
    public string bossStatus;

    public int blockCounter;
    public bool won;
    public bool lost;
    public Canvas endGame;

    public AnimationClip cthuluShake;
    public int combo;

    public Text playerDmg;
    public Text enemyDmg;
    public GameObject playerD;

    public Button quitButton;
    public Button menuButton;
    public float enemyHeal;
    
    // Start is called before the first frame update
    void Start()
    {
        enemyHeal = 0.0f;
        playerDmg.enabled = false;
        enemyDmg.enabled = false;
        cthulu.GetComponent<cthulu>().minion.transform.position = new Vector3(300, 300, 0); 
        transit.enabled = false;
        statusBox.SetActive(false);
        yogScript = GameObject.Find("yog").GetComponent<yog>();
        //cthulu = GameObject.Find("cthulu");
        yogScript.health = 100;
        azathothScript = azathoth.GetComponent<azathoth>();
        endGame.transform.Find("cthuluWin").gameObject.SetActive(false);
        endGame.transform.Find("hasturWin").gameObject.SetActive(false);
        endGame.transform.Find("nyarWin").gameObject.SetActive(false);
        endGame.transform.Find("lost").gameObject.SetActive(false);

        characterSelect.transform.Find("cthInfo").gameObject.SetActive(false);
        characterSelect.transform.Find("hasturInfo").gameObject.SetActive(false);
        characterSelect.transform.Find("nyarInfo").gameObject.SetActive(false);

        lastMove = 0;
        indi1.GetComponent<indicator>().setIndicators(1);
        indi2.GetComponent<indicator>().setIndicators(1);
        indi3.GetComponent<indicator>().setIndicators(1);
        indi4.GetComponent<indicator>().setIndicators(1);

        title.enabled = true;

        if (title)
        {
            title.enabled = true;
            info.enabled = false;
        }
        else
        {
            characterSelect.enabled = true;
        }

        endGame.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerD.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.0f)
        {
            playerD.GetComponent<Animator>().SetBool("New Bool", false);
            playerDmg.enabled = false;
            
        }
        if (enemyDmg.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.0f)
        {
            enemyDmg.GetComponent<Animator>().SetBool("New Bool", false);
            enemyDmg.enabled = false;
            //enemyDmg.GetComponent<Animator>().GetComponent<Animation>()["dmgEnemy"].time = 0.0f;
        }
        if (chosenChar)
        {
            switch (chosenChar.name)
            {
                case "cthulu":
                    combo = chosenChar.GetComponent<cthulu>().combo;
                    if (cthulu.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
                    {
                        cthulu.GetComponent<Animator>().SetBool("New Bool", false);

                        cthuluSprite.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);

                    }
                    if (cthulu.GetComponent<cthulu>().minion.transform.Find("minion").GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
                    {
                        cthulu.GetComponent<cthulu>().minion.transform.Find("minion").GetComponent<Animator>().SetBool("New Bool", false);
                    }
                    break;
                case "hastur":
                    combo = chosenChar.GetComponent<hustar>().combo;
                    if (hustar.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
                    {
                        hustar.GetComponent<Animator>().SetBool("New Bool", false);
                    }
                    break;
                case "nyar":
                    combo = chosenChar.GetComponent<nyar>().combo;
                    if (nyar.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
                    {
                        nyar.GetComponent<Animator>().SetBool("New Bool", false);
                    }
                    break;
            }
        }

        if (yog.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            yog.GetComponent<Animator>().SetBool("New Bool", false);
        }

        if(azathoth.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            azathoth.GetComponent<Animator>().SetBool("New Bool", false);
        }
        
        if (won)
        {
            endGame.enabled = true;
            menuButton.GetComponent<Image>().enabled = true;
            quitButton.GetComponent<Image>().enabled = true;
            switch (chosenChar.name)
            {
                case "cthulu":
                    endGame.transform.Find("cthuluWin").gameObject.SetActive(true);
                    break;
                case "hastur":
                    endGame.transform.Find("hasturWin").gameObject.SetActive(true);
                    break;
                case "nyar":
                    endGame.transform.Find("nyarWin").gameObject.SetActive(true);
                    break;
            }
        }

        if (lost)
        {
            endGame.enabled = true;
            endGame.gameObject.transform.Find("lost").gameObject.SetActive(true);
            menuButton.GetComponent<Image>().enabled = true;
            quitButton.GetComponent<Image>().enabled = true;
        }
    }

    

   

    public void takeTurn(int move)
    {
        playerMove = move;
        if(blockCounter == 0)
        {
            switch (chosenChar.name)
            {
                case "cthulu":
                    chosenChar.GetComponent<cthulu>().moveDisabled = 0;
                    break;
                case "hastur":
                    chosenChar.GetComponent<hustar>().moveDisabled = 0;
                    break;
                case "nyar":
                    chosenChar.GetComponent<nyar>().moveDisabled = 0;
                    break;
            }
        } else
        {
            blockCounter--;
        }
        StartCoroutine(turn());

    }

    public void loadMain() {
        SceneManager.LoadScene("mainGame");
        
    }
    public void quitGame() {
        Application.Quit();
    }

    //have a function to enable info screen and keep characterSelect as disabled
    public void infoScreen()
    {
        characterSelect.enabled = false;
        title.enabled = false;
        info.enabled = true;
    }

    //have a function to return to the title screen
    public void menuScreen()
    {
        info.enabled = false;
        title.enabled = true;
    }

    //move from info to character select screen
    public void characterSelectScreen()
    {
        title.enabled = false;
        info.enabled = false;
        characterSelect.enabled = true;
    }

    public void startGame()
    {
        characterSelect.transform.Find("cthInfo").gameObject.SetActive(false);
        characterSelect.transform.Find("hasturInfo").gameObject.SetActive(false);
        characterSelect.transform.Find("nyarInfo").gameObject.SetActive(false);
        characterSelect.enabled = false;
    }

    public void characterSelected(string name)
    {
        
        switch (name)
        {
            //the player has selected Cthulu
            case "cthulu":
                //apply thumbnail changes
                cthuluThumb.color = Color.green;
                hasturThumb.color = Color.black;
                nyarThumb.color = Color.black;
                //display monster info
                characterSelect.transform.Find("cthInfo").gameObject.SetActive(true);
                characterSelect.transform.Find("hasturInfo").gameObject.SetActive(false);
                characterSelect.transform.Find("nyarInfo").gameObject.SetActive(false);
                chosenChar = cthulu.gameObject;
               
                break;
            //the player has selected Hastur
            case "hastur":
                //apply thumbnail changes
                cthuluThumb.color = Color.black;
                hasturThumb.color = Color.green;
                nyarThumb.color = Color.black;
                //display monster info
                characterSelect.transform.Find("cthInfo").gameObject.SetActive(false);
                characterSelect.transform.Find("hasturInfo").gameObject.SetActive(true);
                characterSelect.transform.Find("nyarInfo").gameObject.SetActive(false);
                chosenChar = hustar.gameObject;

                break;
            //the player has selected nyarlarthotep
            case "nyar":
                //thumbnail changes
                cthuluThumb.color = Color.black;
                nyarThumb.color = Color.green;
                hasturThumb.color = Color.black;
                //display monster info
                characterSelect.transform.Find("cthInfo").gameObject.SetActive(false);
                characterSelect.transform.Find("hasturInfo").gameObject.SetActive(false);
                characterSelect.transform.Find("nyarInfo").gameObject.SetActive(true);
                chosenChar = nyar.gameObject;

               
                break;
        }
        
    }

    public void start()
    {
        menuButton.GetComponent<Image>().enabled = false;
        quitButton.GetComponent<Image>().enabled = false;
        cthulu.GetComponent<cthulu>().summoned = false;
        cthulu.GetComponent<cthulu>().minion.transform.position = new Vector3(300, 300, 0);
        lost = false;
        won = false;
        endGame.transform.Find("cthuluWin").gameObject.SetActive(false);
        endGame.transform.Find("hasturWin").gameObject.SetActive(false);
        endGame.transform.Find("nyarWin").gameObject.SetActive(false);
        endGame.transform.Find("lost").gameObject.SetActive(false);


        characterSelect.enabled = false;
        switch (chosenChar.name)
        {
            case "cthulu":
                move1.text = "Jab";
                move2.text = "Titan Drop";
                move3.text = "Minion Summon";
                move4.text = "Regeneration";
                chosenChar.GetComponent<cthulu>().activeChar = true;
                nyar.GetComponent<nyar>().activeChar = false;
                hustar.GetComponent<hustar>().activeChar = false;
                chosenChar.GetComponent<cthulu>().health = 100;
                indi2.SetActive(false);
                indi3.SetActive(false);
                indi1.SetActive(true);
                indi4.SetActive(true);
                cthulu.GetComponent<cthulu>().multiplier = 1.0f;
                break;
            case "nyar":
                move1.text = "Mimicry";
                move2.text = "Reveal Self";
                move3.text = "Elevated Siphon";
                move4.text = "Concussed Staff";
                chosenChar.GetComponent<nyar>().activeChar = true;
                hustar.GetComponent<hustar>().activeChar = false;
                cthulu.GetComponent<cthulu>().activeChar = false;
                chosenChar.GetComponent<nyar>().health = 100;
                indi1.SetActive(false);
                indi2.SetActive(true);
                indi3.SetActive(true);
                indi4.SetActive(true);
                nyar.GetComponent<nyar>().multiplier = 1.0f;
                break;
            case "hastur":
                move1.text = "Flame";
                move2.text = "Siphon";
                move3.text = "Yellow Sigil";
                move4.text = "Refracted Reality";
                chosenChar.GetComponent<hustar>().activeChar = true;
                nyar.GetComponent<nyar>().activeChar = false;
                cthulu.GetComponent<cthulu>().activeChar = false;
                chosenChar.GetComponent<hustar>().health = 100;
                indi4.SetActive(false);
                indi2.SetActive(true);
                indi3.SetActive(true);
                indi1.SetActive(true);
                hustar.GetComponent<hustar>().multiplier = 1.0f;
                break;
        }
        indi1.GetComponent<indicator>().setIndicators(1);
        indi2.GetComponent<indicator>().setIndicators(1);
        indi3.GetComponent<indicator>().setIndicators(1);
        indi4.GetComponent<indicator>().setIndicators(1);

        yogScript.activeChar = true;
        yogScript.multiplier = 1.0f;
        azathothScript.multiplier = 1.0f;
        //Instantiate(chosenChar, new Vector3(-5, 0, 0), Quaternion.identity);
        switch (chosenChar.name)
        {
            case "cthulu":
                chosenChar.transform.position = new Vector3(-6f, -0.5f, 0);
                break;
            case "hastur":
                chosenChar.transform.position = new Vector3(-4.5f, 0.5f, 0f);
                break;
            case "nyar":
                chosenChar.transform.position = new Vector3(-4.5f, 1, 0);
                break;
        }
        
        currentEnemy.transform.position = new Vector3(6.5f, 0, 0);
        //yogScript.health = 10;
    }

    public IEnumerator turn()
    {
        playerDmg.text = "";
        enemyDmg.text = "";
        switch (chosenChar.name)
        {
            case "cthulu":
                damage = cthulu.GetComponent<cthulu>().handleMoves(playerMove);
                combo = cthulu.GetComponent<cthulu>().combo;
                break;
            case "hastur":
                damage = chosenChar.GetComponent<hustar>().handleMoves(playerMove);
                break;
            case "nyar":
                damage = chosenChar.GetComponent<nyar>().handleMoves(playerMove);
                break;

        }

        
        
        if (currentEnemy.name == "yog")
        {
            if (damage > 0)
            {
                enemyDmg.text = damage.ToString();
                enemyDmg.GetComponent<Animator>().SetBool("New Bool", true);
                enemyDmg.enabled = true;
                yogScript.health -= damage;
                yog.GetComponent<Animator>().SetBool("New Bool", true);
            }
            
            yogScript.healthBar.rectTransform.sizeDelta = new Vector2(yogScript.health, 100);
            if (blockCounter != 0)
            {
                enemyMove = (int)Random.Range(2, 5);
            } else
            {
                enemyMove = (int)Random.Range(1, 5);
            }
            
            damage = yogScript.handleMoves(enemyMove);
            
        }
        else if(currentEnemy.name == "azathoth")
        {
            if(damage > 0)
            {
                azathothScript.health -= damage;
                azathoth.GetComponent<Animator>().SetBool("New Bool", true);
                enemyDmg.text = damage.ToString();
                enemyDmg.GetComponent<Animator>().SetBool("New Bool", true);
                enemyDmg.enabled = true;
            }

            //azathothScript.healthBar.GetComponent<Rect>().size.Set(azathothScript.health, 5);
            azathothScript.healthBar.rectTransform.sizeDelta = new Vector2(azathothScript.health, 100);
            if (blockCounter > 0)
            {
                enemyMove = (int)Random.Range(2, 4);
            } else
            {
                switch (chosenChar.name)
                {
                    case "cthulu":
                        if (cthulu.GetComponent<cthulu>().health < 30)
                        {
                            enemyMove = (int)Random.Range(1, 5);
                        } else
                        {
                            enemyMove = (int)Random.Range(1, 4);
                        }
                        break;
                    case "hastur":
                        if (hustar.GetComponent<hustar>().health < 30)
                        {
                            enemyMove = (int)Random.Range(1, 5);
                        }
                        else
                        {
                            enemyMove = (int)Random.Range(1, 4);
                        }
                        break;
                    case "nyar":
                        if (nyar.GetComponent<nyar>().health < 30)
                        {
                            enemyMove = (int)Random.Range(1, 5);
                        }
                        else
                        {
                            enemyMove = (int)Random.Range(1, 4);
                        }
                        break;
                }
                //enemyMove = (int)Random.Range(1, 4);
            }
            
            damage = azathothScript.handleMoves(enemyMove);
        }
        
        
        statusBox.SetActive(true);
        statusText.text = playerStatus;

        yield return new WaitForSeconds(2.5f);

        switch (currentEnemy.name)
        {
            case "azathoth":
                azathothScript.health += enemyHeal;
                break;
            case "yog":
                yogScript.health += enemyHeal;
                break;
        }
        enemyHeal = 0.0f;
        if (damage > 0)
        {
            playerDmg.text = damage.ToString();
            playerD.GetComponent<Animator>().SetBool("New Bool", true);
            playerDmg.enabled = true;
        }
        if(cthulu.GetComponent<cthulu>().summoned == true&&damage>0)
        {
            bossStatus += " Your minion takes the brunt of the attack";
            cthulu.GetComponent<cthulu>().minion.transform.Find("minion").GetComponent<Animator>().SetBool("New Bool", true);
        }
        statusText.text = bossStatus;
        switch (chosenChar.name)
        {
            case "cthulu":
                //cthulu.GetComponent<Animator>().SetBool("New Bool", false);
                if(cthulu.GetComponent<cthulu>().summoned == false)
                {
                    cthulu.GetComponent<cthulu>().health -= damage;
                    if (damage > 0)
                    {
                        cthulu.GetComponent<Animator>().SetBool("New Bool", true);
                        //cthuluSprite.GetComponent<SpriteRenderer>().color = new Color(233, 41, 41);
                    } 
                } else
                {
                    cthulu.GetComponent<cthulu>().minionHealth -= damage;
                    if (cthulu.GetComponent<cthulu>().minionHealth <= 0)
                    {
                        cthulu.GetComponent<cthulu>().summoned = false;
                        cthulu.GetComponent<cthulu>().minionCounter = 0;
                        cthulu.GetComponent<cthulu>().minion.transform.position = new Vector3(300, 300, 0);
                    }
                }
                                
                //cthulu.GetComponent<Animator>().SetBool("New Bool", false);
                break;
            case "hastur":
                if (hustar.GetComponent<hustar>().shield)
                {
                    hustar.GetComponent<hustar>().health -= damage * 0.7f;
                } else
                {
                    hustar.GetComponent<hustar>().health -= damage;
                }
                if(damage>0)
                hustar.GetComponent<Animator>().SetBool("New Bool", true);
                break;
            case "nyar":
                nyar.GetComponent<nyar>().health -= damage;
                if(damage>0)
                nyar.GetComponent<Animator>().SetBool("New Bool", true);
                break;
        }

        yield return new WaitForSeconds(2.5f);
        
        
        switch (playerMove)
        {
            case 1:

                indi1.GetComponent<indicator>().setIndicators(combo);
                indi2.GetComponent<indicator>().setIndicators(1);
                indi3.GetComponent<indicator>().setIndicators(1);
                indi4.GetComponent<indicator>().setIndicators(1);
                break;
            case 2:
                indi2.GetComponent<indicator>().setIndicators(combo);
                indi3.GetComponent<indicator>().setIndicators(1);
                indi1.GetComponent<indicator>().setIndicators(1);
                indi4.GetComponent<indicator>().setIndicators(1);
                break;
            case 3:
                indi3.GetComponent<indicator>().setIndicators(combo);
                indi2.GetComponent<indicator>().setIndicators(1);
                indi1.GetComponent<indicator>().setIndicators(1);
                indi4.GetComponent<indicator>().setIndicators(1);
                break;
            case 4:
                indi4.GetComponent<indicator>().setIndicators(combo);
                indi2.GetComponent<indicator>().setIndicators(1);
                indi3.GetComponent<indicator>().setIndicators(1);
                indi1.GetComponent<indicator>().setIndicators(1);
                break;
        }
        lastMove = playerMove;
        statusBox.SetActive(false);
        switch (currentEnemy.name)
        {
            case "yog":
                if(yogScript.health <= 0)
                {
                    transition();
                    statusBox.SetActive(false);
                }
                break;
            case "azathoth":
                if(azathothScript.health <= 0)
                {
                    won = true;
                    
                }
                break;
        }
    }
   
    public void returnToMenu()
    {
        title.enabled = true;
        yogScript.health = 100;
        nyar.GetComponent<nyar>().health = 100;
        cthulu.GetComponent<cthulu>().health = 100;
        hustar.GetComponent<hustar>().health = 100;
        azathothScript.health = 100;
        Vector3 neu = new Vector3(300, 300, 300);
        yog.transform.position = neu;
        azathoth.transform.position = neu;
        cthulu.transform.position = neu;
        hustar.transform.position = neu;
        nyar.transform.position = neu;
        menuButton.GetComponent<Image>().enabled = false;
        quitButton.GetComponent<Image>().enabled = false;
        cthulu.GetComponent<cthulu>().moveDisabled = 0;
        nyar.GetComponent<nyar>().moveDisabled = 0;
        hustar.GetComponent<hustar>().moveDisabled = 0;
        yogScript.activeChar = true;
        azathothScript.activeChar = false;
        currentEnemy = yog;
        azathoth.transform.position = new Vector3(300, 300, 0);
        
    }

    public IEnumerator transition()
    {
        transit.enabled = true;
        yield return new WaitForSeconds(2.0f);
        indi1.GetComponent<indicator>().setIndicators(1);
        indi2.GetComponent<indicator>().setIndicators(1);
        indi3.GetComponent<indicator>().setIndicators(1);
        indi4.GetComponent<indicator>().setIndicators(1);
        switch (chosenChar.name)
        {
            case "cthulu":
                cthulu.GetComponent<cthulu>().health = 100;
                break;
            case "hastur":
                hustar.GetComponent<hustar>().health = 100;
                break;
            case "nyar":
                nyar.GetComponent<nyar>().health = 100;
                break;

        }
        currentEnemy = azathoth;
        Vector3 temp = yog.transform.position;
        azathoth.transform.position = new Vector3(6.5f, 0, 0);
        yog.transform.position = new Vector3(300, 300, 0);
        yogScript.health = 100;
        yogScript.activeChar = false;
        azathothScript.activeChar = true;
        azathothScript.health = 100;
        transit.enabled = false;
        cthulu.GetComponent<cthulu>().moveDisabled = 0;
        nyar.GetComponent<nyar>().moveDisabled = 0;
        hustar.GetComponent<hustar>().moveDisabled = 0;
        cthulu.GetComponent<cthulu>().minion.transform.position = new Vector3(300, 300, 0);
        cthulu.GetComponent<cthulu>().summoned = false;
        indi1.GetComponent<indicator>().setIndicators(1);
        indi2.GetComponent<indicator>().setIndicators(1);
        indi3.GetComponent<indicator>().setIndicators(1);
        indi4.GetComponent<indicator>().setIndicators(1);
    }
}
