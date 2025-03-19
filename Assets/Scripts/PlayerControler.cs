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
    private Animator m_Animator;
    private GameObject m_Player;
    public float fireballSpeed = 10f;
    public float fireballDistance = 5f;
    public PlayerControler Player;
    public GameObject projectilePrefab;
    public Transform firePoint;
    private float m_Timer;
    public DamageAura onionWeapon;

    private Rigidbody2D rb;
    private Vector2 movement;

    //Stats
    public int level = 1; //Current level
    public int maxHP = 20; //Max life
    public float hp; //Current life
    public float defence = 0; //Current defence
    public int damage = 1; //Current damage 
    public float critChance = 0; //Current crit chance
    public float critMultiplier = 1.5f; //Current crit Multiplier
    public float attackSpeed = 1; //Current attack speed
    public float rangeMultiplier = 1; //Current range multiplier
    public float moveSpeed = 5f; //Current movement speed



    //Capacité
    //FireCircle
    public int segments = 50; // Nombre de points pour le cercle
    public float radius = 2f; // Rayon de la zone
    private LineRenderer line;
    private bool m_IsCircleOn = false;

    // Start is called before the first frame update
    void Start()
    {
        hp = maxHP;
        m_Animator = this.gameObject.GetComponent<Animator>();
        MenuManager.Instance.HUD.DisplayHP(hp);
        rb = GetComponent<Rigidbody2D>();



        //FireCircle
        onionWeapon.gameObject.SetActive(false);

        line = gameObject.AddComponent<LineRenderer>();
        line.positionCount = segments + 1;
        line.useWorldSpace = false;
        line.startWidth = 0.05f;
        line.endWidth = 0.05f;

    }

    // Update is called once per frame
    void Update()
    {
        m_Timer += Time.deltaTime;

        if (m_Timer >= 5 / attackSpeed)
        {
            ShootFireball();
            m_Timer = 0;

        }
        
        if (hp > 0 && point < maxHP)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                MenuManager.Instance.MenuPause.gameObject.SetActive(!MenuManager.Instance.MenuPause.gameObject.activeSelf);
            }


            // Récupération des inputs
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            // Normalisation pour éviter d'aller plus vite en diagonale
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
            if (level > 10 && m_IsCircleOn == false)
            {
                m_IsCircleOn = true;
                DrawCircle();
                onionWeapon.gameObject.SetActive(true);
            }
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
        hp -= damage ;
        MenuManager.Instance.HUD.DisplayHP(hp);
        if (hp <= 0)
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
    private void Victory()
    {
        Debug.Log("Horra !!!");
        MenuManager.Instance.MenuVictory.gameObject.SetActive(true);
        MenuManager.Instance.MenuVictory.DisplayScore((int)point);
    }

    private void ShootFireball()
    {
        Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
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
        Debug.Log("PlayerLevelUp");
        level += 1;
        maxHP += 1;
        hp += 1;
        attackSpeed += 0.01f;
        moveSpeed += 0.1f;
        rangeMultiplier += 0.01f;
        MenuManager.Instance.HUD.DisplayLevel(level);
        MenuManager.Instance.HUD.DisplayHP(hp);
        //SetRadius((radius * rangeMultiplier / 5) + 1);

    }

    public void FireballAttack()
    {

    }






    void DrawCircle()
    {
        float angle = 0f;
        for (int i = 0; i < segments + 1; i++)
        {
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;
            line.SetPosition(i, new Vector3(x, y, 0));
            angle += 2 * Mathf.PI / segments;
        }
    }
    public void SetRadius(float newRadius)
    {
        radius = newRadius;
        DrawCircle();
    }
}
