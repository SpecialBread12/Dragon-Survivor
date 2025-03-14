using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PlayerControler : MonoBehaviour
{
    public LayerMask WallLayer;
    public Grid Grid;
    public float point;
    public float maxPoint;
    public float PointVie;
    private Animator m_Animator;
    private GameObject m_Player;
    public float fireballSpeed = 10f;
    public float fireballDistance = 5f;
    public GameObject fireballPrefab;
    public PlayerControler Player;

    private float m_Timer;
    public float m_AttackRate;

    public float moveSpeed = 5f; // Vitesse de d�placement

    private Rigidbody2D rb;
    private Vector2 movement;


    public int damage = 1;
    public float attackSpeed = 1;
    public float defence = 0;
    // Start is called before the first frame update
    void Start()
    {
        m_Animator = this.gameObject.GetComponent<Animator>();
        MenuManager.Instance.HUD.DisplayHP(PointVie);
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        m_Timer += Time.deltaTime;

        if (m_Timer >= m_AttackRate)
        {
            ShootFireball();
            m_Timer = 0;

        }

        if (PointVie > 0 && point < maxPoint)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                MenuManager.Instance.MenuPause.gameObject.SetActive(!MenuManager.Instance.MenuPause.gameObject.activeSelf);
            }


            // R�cup�ration des inputs
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            // Normalisation pour �viter d'aller plus vite en diagonale
            if (movement.magnitude > 1)
            {
                movement.Normalize();
            }
            //Debug.Log("Mouvement: " + movement);

            /* Ancien mode de mouvement
            if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                MoveY(-1);
            }
            if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                MoveY(1);
            }
            if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                MoveX(-1);
            }
            if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                MoveX(1);
            }
            */
        }
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
    /*
    public void MoveX(int movementX)
    {

        if (movementX == 1)
        {
            Vector2 moveDirection = new Vector2(1, 0).normalized;
            if (CanMove(moveDirection))
            {
                if (transform.position.x + 1 < Grid.ColumnCount)
                {
                    transform.position = new Vector3(transform.position.x + 1, transform.position.y, 0);
                }
            }
            
            else
            {
                Debug.Log("Wall");
            }
        }
        else
        {
            Vector2 moveDirection = new Vector2(-1, 0).normalized;
            if (CanMove(moveDirection))
            {
                if (transform.position.x - 1 > Grid.transform.position.x)
                {
                    transform.position = new Vector3(transform.position.x - 1, transform.position.y, 0);
                }
            }
            else
            {
                Debug.Log("Wall");
            }
        }
    }

    public void MoveY(int movementY)
    {

        if (movementY == 1)
        {
            Vector2 moveDirection = new Vector2(0, 1).normalized;
            if (CanMove(moveDirection))
            {
                if (transform.position.y + 1 < Grid.RowCount)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y + 1, 0);
                }
            }
            else
            {
                Debug.Log("Wall");
            }
        }
        else
        {
            Vector2 moveDirection = new Vector2(0, -1).normalized;
            if (CanMove(moveDirection))
            {
                if (transform.position.y - 1 > Grid.transform.position.y)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y - 1, 0);
                }
            }
            else
            {
                Debug.Log("Wall");
            }
        }
    }
    
    bool CanMove(Vector2 moveDirection)
    {
        // Adjust the raycast origin to start slightly above the player
        Vector2 raycastOrigin = new Vector2(transform.position.x, transform.position.y) + moveDirection * 0.1f;

        // Raycast to check for a wall
        RaycastHit2D hit = Physics2D.Raycast(raycastOrigin, moveDirection, 1f, WallLayer);

        // If the ray hits something tagged as "Wall," prevent movement
        if (hit.collider != null && hit.collider.CompareTag("Wall"))
        {
            return false;
        }

        // No wall in the way, movement is allowed
        return true;
    }
    */
    public void TakeDamage(float damage)
    {
        m_Animator.SetTrigger("Hit");
        Debug.Log("Hurt");
        PointVie -= damage ;
        MenuManager.Instance.HUD.DisplayHP(PointVie);
        if (PointVie <= 0)
        {
            Debug.Log("You are ded");
            m_Animator.SetBool("IsDead", true);

        }
    }
    public void GameOver()
    {
        MenuManager.Instance.MenuDead.gameObject.SetActive(true);
        MenuManager.Instance.MenuDead.DisplayScore((int)point);
    }

    public void TakePoint(float valeur)
    {
        if (maxPoint > point)
        {
            Debug.Log("Bravo !");
            point += valeur;
            MenuManager.Instance.HUD.DisplayPoint((int)point);
        }

        if (maxPoint <= point)
        {
            Victory();
        }
    }

    private void Victory()
    {
        Debug.Log("Horra !!!");
        MenuManager.Instance.MenuVictory.gameObject.SetActive(true);
        MenuManager.Instance.MenuVictory.DisplayScore((int)point);
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
    public void LevelUp()
    {
        PointVie += 1;
        attackSpeed += 0.01f;
        moveSpeed += 0.1f;

    }
}
