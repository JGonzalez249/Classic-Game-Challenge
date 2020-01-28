using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


/*Wave counter doesn't work, replaced with score based transition instead, very buggy
 return to this later and fix wave counter and implement enemies with shooting mechanics.*/


public class GameController : MonoBehaviour
{
    public GameObject asteroid;

    private int score;
    private int hiscore;
    private int asteroidsRemaining;
    private int lives;
    private int level;
    private int increaseEachWave = 4;

    public Text scoreText;
    public Text livesText;
    public Text waveText;
    public Text hiscoreText;
    public Text gameOverText;
    public Text nextLevelText;
    public Text creditsText;
    public Text restartLevelText;
    public Text winGameText;

    //private bool gameOver;
    private bool winGame;



    // Use this for initialization
    private void Start()
    {
        gameOverText.text = "";
        nextLevelText.text = "";
        creditsText.text = "";
        restartLevelText.text = "";
        winGameText.text = "";

        hiscore = PlayerPrefs.GetInt("hiscore", 0);
        BeginGame();

        if(SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(2))
        {
            BeginGame2();
        }

        //gameOver = false;
        winGame = false;
    }


    // Update is called once per frame
    private void Update()
    {
        // Quit if player presses escape
        if (Input.GetKey("escape"))
            Application.Quit();

        // if (gameOver)
        { 
        
        }
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
                Debug.Log("Loaded Level 2!");
            }
            // Go to Credits
            if (Input.GetKeyDown(KeyCode.C))
            {
                SceneManager.LoadScene(3);
            }

        //Is score greater than 100?
        if (score >= 100)
        {
            winGame = true;
            winGameText.text = "You Win!";
        }

        if (winGame)
        {
            gameOverText.text = "";

            nextLevelText.text = "";

            restartLevelText.text = "Press 'R' to Restart Game";

            creditsText.text = "Press 'C' to go to Credits";

            if (Input.GetKeyDown(KeyCode.C))
            {
                SceneManager.LoadScene(3);
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(0);
            }
        }

    }



    private void BeginGame()
    {
        
        
        score = 0;
        lives = 3;
        level = 1;

        gameOverText.text = "";
        nextLevelText.text = "";
        creditsText.text = "";
        restartLevelText.text = "";
        winGameText.text = "";

        // Prepare the HUD
        scoreText.text = "SCORE:" + score;
        hiscoreText.text = "HI-SCORE: " + hiscore;
        livesText.text = "LIVES: " + lives;
        waveText.text = "LEVEL: " + level;

        SpawnAsteroids();
    }

    private void BeginGame2()
    {
        
        score = 28;
        lives = 3;
        level = 2;

        gameOverText.text = "";
        nextLevelText.text = "";
        creditsText.text = "";
        restartLevelText.text = "";
        winGameText.text = "";

        // Prepare the HUD
        scoreText.text = "SCORE:" + score;
        hiscoreText.text = "HI-SCORE: " + hiscore;
        livesText.text = "LIVES: " + lives;
        waveText.text = "LEVEL: " + level;

        SpawnAsteroids();
    }


    private void SpawnAsteroids()
    {
        DestroyExistingAsteroids();

        // Decide how many asteroids to spawn
        // If any asteroids left over from previous game, subtract them
        asteroidsRemaining = (level * increaseEachWave);

        for (int i = 0; i < asteroidsRemaining; i++)
        {
            // Spawn an asteroid
            Instantiate(asteroid,
                new Vector3(Random.Range(-9.0f, 9.0f),
                    Random.Range(-6.0f, 6.0f), 0),
                Quaternion.Euler(0, 0, Random.Range(-0.0f, 359.0f)));
        }

        waveText.text = "LEVEL: " + level;
    }

    public void IncrementScore()
    {
        score++;

        scoreText.text = "SCORE:" + score;

        // Has player destroyed all asteroids?
        if (asteroidsRemaining < 1)
        {

            // Start next wave
            level++;
            SpawnAsteroids();

        }

        if (score == 28 && SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(1))
        {
            SceneManager.LoadScene(2);
            BeginGame2();
        }
        else if (score == 56 && SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(2))
        {
            SceneManager.LoadScene(3);
        }
        

        if (score > hiscore)
        {
            hiscore = score;
            hiscoreText.text = "HI-SCORE: " + hiscore;

            // Save the new hiscore
            PlayerPrefs.SetInt("hiscore", hiscore);
        }

        
    }

    public void DecrementLives()
    {
        lives--;
        livesText.text = "LIVES: " + lives;

        // Has player run out of lives in Level 1?
        if (lives < 1)
        {
            //gameOver = true;

            gameOverText.text = "Game Over!" + "\n" + "Press 'R' to Restart Game";

            nextLevelText.text = "Press 'G' to go to next Level";

            creditsText.text = "Press 'C' to go to Credits";

            if (Input.GetKeyDown(KeyCode.R))
            {
                BeginGame();
                Debug.Log("Game Restart");
            }
            // Go to the next level
            if (Input.GetKeyDown(KeyCode.G))
            {
                SceneManager.LoadScene(2);
                Debug.Log("Loaded Level 2!");
            }
            // Go to Credits
            if (Input.GetKeyDown(KeyCode.C))
            {
                SceneManager.LoadScene(3);
                Debug.Log("Loaded Credits");
            }

            
        }
        // Has player lost 3 lives in level 2
        else if(lives < 1 && SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(2))
        {
            gameOverText.text = "Game Over!" + "\n" + "Press 'R' to Restart Game";
            if (Input.GetKeyDown(KeyCode.R))
            {
                
                SceneManager.LoadScene(1);
                Debug.Log("Game Restart");
            }

            creditsText.text = "Press 'C' to go to Credits";
            // Go to Credits
            if (Input.GetKeyDown(KeyCode.C))
            {
                SceneManager.LoadScene(3);
                Debug.Log("Loaded Credits");
            }

            restartLevelText.text = "Press 'G' to Restart Level";
            // Go to the next level
            if (Input.GetKeyDown(KeyCode.G))
            {
                SceneManager.LoadScene(2);
                Debug.Log("Loaded Level 2!");
            }



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
        // + 2 little ones
        // = 2
        asteroidsRemaining += 2;
    }

    private void DestroyExistingAsteroids()
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