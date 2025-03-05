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


    private void Awake()
    {
    // m_AS = GetComponent<AudioSource>();
    }
    private void Start()
    {
        //m_AS.PlayOneShot(m_SpawnSound[Random.Range(0, m_SpawnSound.Length)]);
    }
    private void Update()
    {
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
    }
    public void Die()
    {

        

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
            t_Player.TakeDamage(1);
            Debug.Log("Hey");
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.CompareTag("fireball"))
        {
            Health--;
            if (Health <= 0)
            {
                Debug.Log("Hey you ded");
                Destroy(this.gameObject);
            }
        }
        
    }
    
    private void CalculatePath()
    {
        Tile t_StartTile = Grid.GetTile(Grid.WorldToGrid(transform.position));
        Tile t_EndTile = Grid.GetTile(Grid.WorldToGrid(Objectif.position));
        m_Path = Pathfinder.GetPath(t_StartTile, t_EndTile, false);
    }
}

