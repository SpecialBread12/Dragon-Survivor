using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 5f;

    public void TakeDamage(float damage)
    {
        health -= damage - (Time.deltaTime / 10000);
        if (health <= 0)
        {
            Destroy(gameObject); // L'ennemi meurt
        }
    }
}
