using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    public Pathfinder Pathfinder;
    public Grid Grid;
    public Transform Objectif;
    public float Speed = 5f;
    public PlayerControler PlayerControler;
    public float Health;

    //[SerializeField]
    //private float m_Vie = 5;
    //[SerializeField]
    //private AudioClip[] m_SpawnSound;
    //private AudioSource m_AS;
    private Path m_Path;
    public float moveSpeed = 3f; // Vitesse de déplacement de l'ennemi
    private Rigidbody2D rb;
    private Transform player;
    public Experience Experience;
    public float damage;
    private void Awake()
    {
    // m_AS = GetComponent<AudioSource>();
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform; // Assurez-vous que le joueur a bien le tag "Player"
        Experience = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<Experience>();
    }
    void FixedUpdate()
    {
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.velocity = direction * moveSpeed; // Applique la vitesse pour aller vers le joueur
        }
    }
    private void Update()
    {
        if (Health <= 0)
        {
            Die();
        }

        /*
        if (m_Path == null)
            CalculatePath();

        if (m_Path == null)
            return;



        Vector3 t_TargetPos = m_Path.Checkpoints[1].transform.position;
        float t_Step = Speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, t_TargetPos, t_Step);

        //Checkpoint
        if (transform.position == t_TargetPos)
        {
            CalculatePath();

            if (m_Path == null) return;

            if (m_Path.Checkpoints.Count == 1)
            {
                PlayerControler.TakeDamage(1);
                Debug.Log("ASS");
                Destroy(this.gameObject);
            }
        }
        */
    }
    public void TakeDamage(float damage)
    {
        Health -= damage;
    }
    public void Die()
    {
        Debug.Log("Ennemi died");
        Experience.GainExperience(2);
        Destroy(this.gameObject);
    }
    /*
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;

        for (int i = 0; i < m_Path.Checkpoints.Count - 1; i++)
        {
            Gizmos.DrawLine(m_Path.Checkpoints[i].transform.position, m_Path.Checkpoints[i + 1].transform.position);
        }
    }
    */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OnTriggerEnter2D - " + collision.gameObject.name);
        PlayerControler t_Player = collision.attachedRigidbody?.gameObject.GetComponent<PlayerControler>();
        if (t_Player != null)
        {
            t_Player.TakeDamage(damage - t_Player.defence);
            Debug.Log("Hey");
            //Destroy(this.gameObject);
        }
        else if (collision.gameObject.CompareTag("Attack"))
        {
            
        }
        
    }
    /*
    private void CalculatePath()
    {
        Tile t_StartTile = Grid.GetTile(Grid.WorldToGrid(transform.position));
        Tile t_EndTile = Grid.GetTile(Grid.WorldToGrid(Objectif.position));
        m_Path = Pathfinder.GetPath(t_StartTile, t_EndTile, false);
    }
    */
}

