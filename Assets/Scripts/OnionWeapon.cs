using UnityEngine;
using System.Collections;

public class DamageAura : MonoBehaviour
{
    public float damagePerSecond = 10f; // Dégâts infligés par seconde

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ennemy")) // Assurez-vous que vos ennemis ont bien le tag "Enemy"
        {
            StartCoroutine(DamageEnemy(other));
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ennemy"))
        {
            StopAllCoroutines();
        }
    }

    private IEnumerator DamageEnemy(Collider2D enemy)
    {
        while (enemy != null)
        {
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damagePerSecond * Time.deltaTime);
            }
            yield return null; // Attendre le prochain frame pour continuer les dégâts
        }
    }
}
