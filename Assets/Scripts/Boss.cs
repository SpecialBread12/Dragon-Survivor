using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;

public class Boss : MonoBehaviour
{
    public Grid Grid;
    public Transform Target;
    public float Speed = 5f;
    public PlayerControler PlayerControler;

    public float Damage = 3f;
    public float Cooldown = 1f;
    private LineRenderer m_Laser;
    private float m_LaserCooldown = 0.05f;
    private float m_LastFire;
    public GameObject RewardPrefab;
    [SerializeField]
    private float m_Vie = 5;

    public float speed = 2.0f;
    private bool movingUp = true;


    private void Awake()
    {
        m_Laser = GetComponent<LineRenderer>();
        m_Laser.enabled = false;
    }
    private void Start()
    {

    }

    private void Update()
    {
        MoveUpDown();

        //Debug.Log(Grid.WorldToGrid(transform.position).y);

        if (m_Laser && Time.time > m_LastFire + m_LaserCooldown)
        {
            m_Laser.enabled = false;
        }

        if (Time.time >= m_LastFire + Cooldown &&  Grid.WorldToGrid(transform.position).y == Grid.WorldToGrid(Target.position).y)
        {
            Fire();
        }



    }
    public void Die()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("fireball"))
        {
            m_Vie--;
            if (m_Vie <= 0)
            {
                Destroy(gameObject);
                //PlayerControler.TakePoint(1000);
                MenuManager.Instance.MenuVictory.gameObject.SetActive(true);
            }
        }
    }

    void MoveUpDown()
    {
        if (movingUp)
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime); // Move upwards
        }
        else
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime); // Move downwards
        }

        // Change direction when reaching certain points
        if (transform.position.y >= Grid.RowCount + Grid.transform.position.y - 1)
        {
            movingUp = false;
        }
        else if (transform.position.y <= Grid.transform.position.y + 1)
        {
            movingUp = true;
        }

    }

    private void Fire()
    {
        Debug.Log("Bang");

        m_LastFire = Time.time;
        PlayerControler.TakeDamage((int)Damage);

        m_Laser.SetPositions(new Vector3[] { transform.position, Target.transform.position });
        m_Laser.enabled = true;

    }

}
