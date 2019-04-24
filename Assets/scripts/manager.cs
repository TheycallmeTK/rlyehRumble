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

    public GameObject cthulu;
    public GameObject hustar;
    public GameObject nyar;
    public GameObject yog;
    public GameObject azathoth;
    public GameObject chosenChar;
    public GameObject currentEnemy;

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
    
    // Start is called before the first frame update
    void Start()
    {
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
        if (won)
        {
            endGame.enabled = true;
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

        if (lost && won == false)
        {
            endGame.enabled = true;
            endGame.gameObject.transform.Find("lost").gameObject.SetActive(true);
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
        characterSelect.enabled = false;
        Debug.Log(chosenChar.name);
        switch (chosenChar.name)
        {
            case "cthulu":
                move1.text = "Jab";
                move2.text = "Titan Drop";
                move3.text = "Minion Summon";
                move4.text = "Regeneration";
                chosenChar.GetComponent<cthulu>().activeChar = true;
                break;
            case "nyar":
                move1.text = "Mimicry";
                move2.text = "Reveal Self";
                move3.text = "Elevated Siphon";
                move4.text = "Concussed Staff";
                chosenChar.GetComponent<nyar>().activeChar = true;
                break;
            case "hastur":
                move1.text = "Flame";
                move2.text = "Siphon";
                move3.text = "Yellow Sigil";
                move4.text = "Refracted Reality";
                chosenChar.GetComponent<hustar>().activeChar = true;
                break;
        }
        yogScript.activeChar = true;
        //Instantiate(chosenChar, new Vector3(-5, 0, 0), Quaternion.identity);
        chosenChar.transform.position = new Vector3(-6f, -1, 0);
        currentEnemy.transform.position = new Vector3(6f, -1, 0);
        //Instantiate(yog, new Vector3(5, 0, 0), Quaternion.identity);
    }

    public IEnumerator turn()
    {
        switch (chosenChar.name)
        {
            case "cthulu":
                damage = cthulu.GetComponent<cthulu>().handleMoves(playerMove);
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
            yogScript.health -= damage;
            Debug.Log("damage: " + damage);
            yogScript.healthBar.rectTransform.sizeDelta = new Vector2(yogScript.health, 100);
            Debug.Log(yogScript.health);
            enemyMove = (int)Random.Range(1, 5);
            damage = yogScript.handleMoves(enemyMove);

        }
        else
        {
            azathothScript.health -= damage;
            yogScript.healthBar.GetComponent<Rect>().size.Set(azathothScript.health, 5);
            if (blockCounter > 0)
            {
                enemyMove = (int)Random.Range(2, 5);
            } else
            {
                enemyMove = (int)Random.Range(1, 5);
            }
            
            damage = azathothScript.handleMoves(enemyMove);
        }
        statusBox.SetActive(true);
        statusText.text = playerStatus;

        yield return new WaitForSeconds(1.5f);

        statusText.text = bossStatus;
        switch (chosenChar.name)
        {
            case "cthulu":
                cthulu.GetComponent<cthulu>().health -= damage;
                break;
            case "hastur":
                if (hustar.GetComponent<hustar>().shield)
                {
                    hustar.GetComponent<hustar>().health -= damage * 0.7f;
                } else
                {
                    hustar.GetComponent<hustar>().health -= damage;
                }
                break;
            case "nyar":
                nyar.GetComponent<nyar>().health -= damage;
                break;
        }

        yield return new WaitForSeconds(1.5f);
        statusBox.SetActive(false);
    }
   
    public void returnToMenu()
    {
        SceneManager.LoadScene("startScreen");
    }
}
