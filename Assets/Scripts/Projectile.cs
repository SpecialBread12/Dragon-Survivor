using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ennemy"))
        {
            // Destroy the fireball
            Destroy(gameObject);
        }
    }
}
