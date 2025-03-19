using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerObjets : MonoBehaviour
{
    public GameObject[] Objets_Prefabs;
    public float IntervalMin, IntervalMax;
    public float timeElapsed = 0f;

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
        timeElapsed += Time.deltaTime;
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

        GameObject t_Creacture = Instantiate(Objets_Prefabs[Random.Range(0, Objets_Prefabs.Length)], transform.position, Quaternion.identity);

        Enemy t_Ennemy = t_Creacture.GetComponent<Enemy>();
        t_Ennemy.Health += timeElapsed / 30; 
    }
  }
