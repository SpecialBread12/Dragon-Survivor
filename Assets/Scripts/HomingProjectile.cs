using UnityEngine;

public class HomingProjectile : MonoBehaviour
{
    public float speed = 5f;
    public float detectionRadius = 5f;
    public float lifetime = 3f;
    private Transform target;
    private Enemy enemy;

    void Start()
    {
        target = FindClosestEnemy();
        Destroy(gameObject, lifetime); // Détruit le projectile après un certain temps
    }

    void Update()
    {
        if (target != null)
        {
            // Déplace le projectile vers la cible
            Vector2 direction = (target.position - transform.position).normalized;
            transform.position += (Vector3)(direction * speed * Time.deltaTime);
        }
    }

    private Transform FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Ennemy");
        Transform closest = null;
        float closestDistance = detectionRadius;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closest = enemy.transform;
                closestDistance = distance;
            }
        }
        return closest;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ennemy"))
        {
            Enemy t_Enemy = other.attachedRigidbody?.gameObject.GetComponent<Enemy>();
            t_Enemy.TakeDamage(1);
            //Destroy(other.gameObject); // Détruit l'ennemi
            Destroy(gameObject); // Détruit le projectile
        }
    }
}
