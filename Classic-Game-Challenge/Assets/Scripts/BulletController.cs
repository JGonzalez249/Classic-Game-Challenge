using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        // Set the bullet to destroy itself after 1 seconds
        Destroy(gameObject, 1.0f);

        // Pushes the bullet in the direction it's facing
        GetComponent<Rigidbody2D>()
            .AddForce(transform.up * 400);
    }

}
