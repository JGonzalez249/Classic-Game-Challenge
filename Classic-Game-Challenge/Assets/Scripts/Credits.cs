using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{
    //Credits and Return to Main Menu
    public Text creditsText;
    public Text returnText;
    public Text quitText;

    //Load Main Menu
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

    // Start is called before the first frame update
    private void Start()
    {
        creditsText.text = "Game Created by Jonathan Gonzalez" + "\n" + "Gabriel Malaret" + "\n" + "and Zachary Goodwin";
        returnText.text = " Main Menu";
        quitText.text = "Quit";

    }

    // Update is called once per frame
    private void Update()
    {

        if (Input.GetKeyDown("escape"))
        {
            Debug.Log("Quit");
            Application.Quit();

        }
    }
}
