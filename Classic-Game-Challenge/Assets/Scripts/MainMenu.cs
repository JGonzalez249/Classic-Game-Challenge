using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{
    //Main Menu Title and Button Text
    public Text asteroidsText;
    public Text playText;
    public Text quitText;
    public Text creditsText;
    
    //Load level 1
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void CreditsGame()
    {
        SceneManager.LoadScene(3);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

    private void Start()
    {
        asteroidsText.text = "Asteroids";
        playText.text = "Play";
        quitText.text = "Quit Game";
        creditsText.text = "Credits Page";
    }

    private void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            Debug.Log("Quit");
            Application.Quit();

        }
    }
}
