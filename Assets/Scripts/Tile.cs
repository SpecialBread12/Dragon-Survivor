using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tile : MonoBehaviour
{
    public bool IsEntre;
    public bool IsSortie;
    public bool Exitlevel1;
    public bool ExitBoss;
    public uint x, y;
    public uint BaseCost;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsEntre)
        {
            Debug.Log("OnTriggerEnter2D - " + collision.gameObject.name);
            PlayerControler t_Player = collision.attachedRigidbody?.gameObject.GetComponent<PlayerControler>();
            Debug.Log("entre");
            if (t_Player != null)
            {
                SceneManager.LoadScene("Dungeon", LoadSceneMode.Single);
            }
        }
        else if (IsSortie)
        {
            Debug.Log("OnTriggerEnter2D - " + collision.gameObject.name);
            PlayerControler t_Player = collision.attachedRigidbody?.gameObject.GetComponent<PlayerControler>();
            Debug.Log("exit");
            if (t_Player != null)
            {
                SceneManager.LoadScene("End", LoadSceneMode.Single);
            }
        }
        else if (Exitlevel1)
        {
            Debug.Log("OnTriggerEnter2D - " + collision.gameObject.name);
            PlayerControler t_Player = collision.attachedRigidbody?.gameObject.GetComponent<PlayerControler>();
            Debug.Log("exitlevel1");
            if (t_Player != null)
            {
                SceneManager.LoadScene("ProceduralForest", LoadSceneMode.Single);
            }
        }
        else if (ExitBoss)
        {
            Debug.Log("OnTriggerEnter2D - " + collision.gameObject.name);
            PlayerControler t_Player = collision.attachedRigidbody?.gameObject.GetComponent<PlayerControler>();
            Debug.Log("exitBoss");
            if (t_Player != null)
            {
                SceneManager.LoadScene("ProceduralForest", LoadSceneMode.Single);
            }
        }
    }
}

