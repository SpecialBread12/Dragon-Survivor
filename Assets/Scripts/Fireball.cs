using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{

    public float fireballSpeed = 10f;
    public float fireballDistance = 5f;
    public GameObject fireballPrefab;
    public PlayerControler Player;

    private float m_Timer;
    public float m_AttackRate;

    void Update()
    {
        if (Player.hp > 0 && Player.point < Player.maxHP)
        {
            m_Timer += Time.deltaTime;

            if (m_Timer >= m_AttackRate)
            {
                ShootFireball();
                m_Timer = 0;

            }
            
            
        }
    }

    private void ShootFireball()
    {
        GameObject fireball = Instantiate(fireballPrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * fireballSpeed;

        // Destroy the fireball after reaching a certain distance
        Destroy(fireball, fireballDistance / fireballSpeed);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy t_Ennemy = collision.attachedRigidbody?.gameObject.GetComponent<Enemy>();
        if (t_Ennemy != null)
        {
            Debug.Log("hit");
        }
    }
}
