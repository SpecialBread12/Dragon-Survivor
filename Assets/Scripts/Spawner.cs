using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] Ennemy_Prefabs;
    public float IntervalMin, IntervalMax;

    public Pathfinder Pathfinder;
    public Grid Grid;
    public Transform Objectif;
    public PlayerControler PlayerControler;

    private float m_Timer;
    private float m_Interval;

    // Start is called before the first frame update
    void Start()
    {
        m_Interval = Random.Range(IntervalMin, IntervalMax);
    }

    // Update is called once per frame
    void Update()
    {
        m_Timer += Time.deltaTime;

        if (m_Timer >= m_Interval)
        {
            Spawn();
            m_Timer = 0;
            m_Interval = Random.Range(IntervalMin, IntervalMax);
        }
    }

    void Spawn()
    {
        Debug.Log(gameObject.name + "- Spawm");

        GameObject t_Creacture = Instantiate(Ennemy_Prefabs[Random.Range(0, Ennemy_Prefabs.Length)], transform.position, Quaternion.identity);

        Enemy t_Ennemy = t_Creacture.GetComponent<Enemy>();

        t_Ennemy.GetComponent<Enemy>().Pathfinder = Pathfinder;
        t_Ennemy.GetComponent<Enemy>().Grid = Grid;
        t_Ennemy.GetComponent<Enemy>().Objectif = Objectif;
        t_Ennemy.GetComponent<Enemy>().PlayerControler = PlayerControler;

    }
}

