using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level2Controller : MonoBehaviour
{

    public GameObject asteroid;

    private int score;
    private int hiscore;
    private int asteroidsRemaining;
    private int lives;
    private int wave;
    private int increaseEachWave = 4;

    public Text scoreText;
    public Text livesText;
    public Text waveText;
    public Text hiscoreText;
    public Text gameOverText;
    public Text nextLevelText;
    public Text creditsText;
    public Text restartLevelText;

    private bool gameOver;
    //private bool restart;

    private PlayerContoller playerContoller;

    // Use this for initialization
    void Start()
    {
        GameObject playerControllerObject = GameObject.FindWithTag("Player");
        if (playerControllerObject != null)
        {
            playerContoller = playerControllerObject.GetComponent<PlayerContoller>();
        }
        if (playerContoller == null)
        {
            Debug.Log("Cannot find 'PlayerController' script");
        }

        hiscore = PlayerPrefs.GetInt("hiscore", 0);
        BeginGame();
        gameOverText.text = "";
        nextLevelText.text = "";
        creditsText.text = "";
        restartLevelText.text = "";

        gameOver = false;
        //restart = false;
    }

    // Update is called once per frame
    void Update()
    {


        // Quit if player presses escape
        if (Input.GetKey("escape"))
            Application.Quit();

        if (gameOver)
        {
            gameOverText.text = "Game Over!" + "\n" + "Press 'R' to Restart Game";

            nextLevelText.text = "Press 'G' to go to next Level";

            creditsText.text = "Press 'C' to go to Credits";

            // Restart the game
            if (Input.GetKeyDown(KeyCode.R))
            {
                BeginGame();
                Debug.Log("Game Restart");
            }
            // Go to the next level
            if (Input.GetKeyDown(KeyCode.G))
            {
                SceneManager.LoadScene(2);
            }
            // Go to Credits
            if (Input.GetKeyDown(KeyCode.C))
            {
                SceneManager.LoadScene(3);
            }
        }

    }


    void BeginGame()
    {
        SceneManager.LoadScene(1);
        score = 0;
        lives = 3;
        wave = 1;

        // Prepare the HUD
        scoreText.text = "SCORE:" + score;
        hiscoreText.text = "HISCORE: " + hiscore;
        livesText.text = "LIVES: " + lives;
        waveText.text = "WAVE: " + wave;


        SpawnAsteroids();
    }



    void SpawnAsteroids()
    {

        DestroyExistingAsteroids();

        // Decide how many asteroids to spawn
        // If any asteroids left over from previous game, subtract them
        asteroidsRemaining = (wave * increaseEachWave);

        for (int i = 0; i < asteroidsRemaining; i++)
        {

            // Spawn an asteroid
            Instantiate(asteroid,
                new Vector3(Random.Range(-9.0f, 9.0f),
                    Random.Range(-6.0f, 6.0f), 0),
                Quaternion.Euler(0, 0, Random.Range(-0.0f, 359.0f)));

        }

        waveText.text = "WAVE: " + wave;

    }




    public void IncrementScore()
    {
        score++;

        scoreText.text = "SCORE:" + score;

        if (score > hiscore)
        {
            hiscore = score;
            hiscoreText.text = "HISCORE: " + hiscore;

            // Save the new hiscore
            PlayerPrefs.SetInt("hiscore", hiscore);
        }

        // Has player destroyed all asteroids?
        if (asteroidsRemaining < 1)
        {

            // Start next wave
            wave++;
            SpawnAsteroids();

            // Has 3 waves past?
            if (wave > 3)
            {
                SceneManager.LoadScene(2);
            }

        }
    }

    public void DecrementLives()
    {
        lives--;
        livesText.text = "LIVES: " + lives;


        // Has player run out of lives in Level 1?
        if (lives < 1)
        {

            gameOver = true;

        }
    }


    public void DecrementAsteroids()
    {
        asteroidsRemaining--;
    }

    public void SplitAsteroid()
    {
        // Two extra asteroids
        // - big one
        // + 3 little ones
        // = 2
        asteroidsRemaining += 2;

    }

    void DestroyExistingAsteroids()
    {
        GameObject[] asteroids =
            GameObject.FindGameObjectsWithTag("Large Asteroid");

        foreach (GameObject current in asteroids)
        {
            GameObject.Destroy(current);
        }

        GameObject[] asteroids2 =
            GameObject.FindGameObjectsWithTag("Mid Asteroid");

        foreach (GameObject current in asteroids2)
        {
            GameObject.Destroy(current);
        }

        GameObject[] asteroids3 =
            GameObject.FindGameObjectsWithTag("Small Asteroid");

        foreach (GameObject current in asteroids3)
        {
            GameObject.Destroy(current);
        }
    }


}
