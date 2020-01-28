using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibiltyController : MonoBehaviour
{

    Renderer rend;
    Color c;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        c = rend.material.color;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("large asteroid") || (collision.gameObject.name.Equals("mid asteroid") || (collision.gameObject.name.Equals("small asteroid"))))
        {
            StartCoroutine("GetInvincible");
        }
    }

}
