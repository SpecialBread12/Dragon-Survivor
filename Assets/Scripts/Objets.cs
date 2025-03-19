using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objets : MonoBehaviour
{
    public float Valeur;
    void Start()
    {
        Debug.Log("Argent start");
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OnTriggerEnter2D -" + collision.gameObject.name);

        PlayerControler t_Player = collision.attachedRigidbody?.gameObject.GetComponent<PlayerControler>();

        if (t_Player != null)
        {
            //Appeler une fonction de point sur le joueur
            //t_Player.TakePoint(Valeur);

            Destroy(gameObject);
        }

    }
}
