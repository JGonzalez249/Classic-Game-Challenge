using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContoller : MonoBehaviour
{
    float rotationSpeed = 200.0f;
    float thrustForce = 1.5f;

    public float fireRate;
    private float nextFire;
   

    public AudioClip crash;
    public AudioClip shoot;

    public GameObject bullet;

    

    private GameController gameController;

    void Start()
    {
        // Reference to the game controller object and the script
        GameObject gameControllerObject =
            GameObject.FindWithTag("GameController");

        gameController =
            gameControllerObject.GetComponent<GameController>();
    }

    private void Update()
    {

        // Has a bullet been fired
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            ShootBullet();
        }
    }

    void FixedUpdate()
    {

        // Rotate the ship in relative x-axis
        transform.Rotate(0, 0, -Input.GetAxis("Horizontal") *
            rotationSpeed * Time.deltaTime);

        // Thrust the ship in relative y-axis
        GetComponent<Rigidbody2D>().
            AddForce(transform.up * thrustForce *
                Input.GetAxis("Vertical"));

    }

    void OnTriggerEnter2D(Collider2D c)
    {

        // Anything except a bullet is an asteroid or an enemy
        if (c.gameObject.tag != "Bullet")
        {

            AudioSource.PlayClipAtPoint
                (crash, Camera.main.transform.position);

            // Move the ship to the centre of the screen
            transform.position = new Vector3(0, 0, 0);

            // Remove all velocity from the ship
            GetComponent<Rigidbody2D>().
                velocity = new Vector3(0, 0, 0);

            gameController.DecrementLives();
        }
    }

    void ShootBullet()
    {

        // Spawn a bullet
        Instantiate(bullet,
            new Vector3(transform.position.x, transform.position.y, 0),
            transform.rotation);

        // Play a shoot sound
        AudioSource.PlayClipAtPoint(shoot, Camera.main.transform.position);
    }
}
