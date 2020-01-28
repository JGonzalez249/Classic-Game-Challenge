﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    public AudioClip destroy;
    public GameObject midAsteroid;
    public GameObject smallAsteroid;

    private Level1Controller level1Controller;

    // Use this for initialization
    void Start()
    {

        // Get a reference to the game controller object and the script
        GameObject level1ControllerObject =
            GameObject.FindWithTag("Level1Controller");

        level1Controller =
            level1Controller.GetComponent<Level1Controller>();

        // Push the asteroid in the direction it is facing
        GetComponent<Rigidbody2D>()
            .AddForce(transform.up * Random.Range(-50.0f, 150.0f));

        // Give a random angular velocity/rotation
        GetComponent<Rigidbody2D>()
            .angularVelocity = Random.Range(-0.0f, 90.0f);

    }

    void OnCollisionEnter2D(Collision2D c)
    {

        if (c.gameObject.tag.Equals("Bullet"))
        {

            // Destroy the bullet
            Destroy(c.gameObject);

            // If large asteroid spawn mid new ones
            if (tag.Equals("Large Asteroid"))
            {
                // Spawn mid asteroids
                Instantiate(midAsteroid,
                    new Vector3(transform.position.x - .5f,
                        transform.position.y - .5f, 0),
                        Quaternion.Euler(0, 0, 90));

                // Spawn mid asteroids-disabled
                /*Instantiate(midAsteroid,
                    new Vector3(transform.position.x + .5f,
                        transform.position.y + .0f, 0),
                        Quaternion.Euler(0, 0, 0));*/

                // Spawn mid asteroids
                Instantiate(midAsteroid,
                    new Vector3(transform.position.x + .5f,
                        transform.position.y - .5f, 0),
                        Quaternion.Euler(0, 0, 270));

                level1Controller.SplitAsteroid(); // +2

            }

            // If mid asteroid spawn small new ones
            if (tag.Equals("Mid Asteroid"))
            {
                // Spawn small asteroids
                Instantiate(smallAsteroid,
                    new Vector3(transform.position.x - .5f,
                        transform.position.y - .5f, 0),
                        Quaternion.Euler(0, 0, 90));

                // Spawn small asteroids - disabled
                /*Instantiate(smallAsteroid,
                    new Vector3(transform.position.x + .5f,
                        transform.position.y + .0f, 0),
                        Quaternion.Euler(0, 0, 0));*/

                // Spawn small asteroids
                Instantiate(smallAsteroid,
                    new Vector3(transform.position.x + .5f,
                        transform.position.y - .5f, 0),
                        Quaternion.Euler(0, 0, 270));

                level1Controller.SplitAsteroid(); // +2

            }
            else
            {
                // Just a small asteroid destroyed
                level1Controller.DecrementAsteroids();
            }

            // Play a sound
            AudioSource.PlayClipAtPoint(
                destroy, Camera.main.transform.position);

            // Add to the score
            level1Controller.IncrementScore();

            // Destroy the current asteroid
            Destroy(gameObject);

        }

    }
}
