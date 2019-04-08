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
    // Start is called before the first frame update
    void Start()
    {
        title.enabled = true;
        info.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeTurn()
    {

    }

    public void loadMain() {
        SceneManager.LoadScene("mainGame");
        
    }
    public void quitGame() {
        Application.Quit();
    }
    public void infoScreen()
    {
        title.enabled = false;
        info.enabled = true;
    }

    public void startGame()
    {
        characterSelect.enabled = false;
    }

    public void characterSelected(string name)
    {
        
        switch (name)
        {
            case "cthulu":
                cthuluThumb.color = Color.green;
                hasturThumb.color = Color.black;
                nyarThumb.color = Color.black;
                break;
            case "hastur":
                cthuluThumb.color = Color.black;
                hasturThumb.color = Color.green;
                nyarThumb.color = Color.black;
                break;
            case "nyar":
                cthuluThumb.color = Color.black;
                nyarThumb.color = Color.green;
                hasturThumb.color = Color.black;
                break;
        }
        
    }
}
