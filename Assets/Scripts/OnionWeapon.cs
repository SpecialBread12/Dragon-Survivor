using UnityEngine;
using System.Collections;

public class DamageAura : MonoBehaviour
{
    public float damagePerSecond = 10f;
    public float auraSize = 1f; // Taille du cercle
    private CircleCollider2D circleCollider;
    private SpriteRenderer auraRenderer;

    void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        auraRenderer = GetComponentInChildren<SpriteRenderer>(); // Récupère le sprite de l'aura
        UpdateAuraSize();
    }
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
    public void SetAuraSize(float newSize)
    {
        auraSize = newSize;
        UpdateAuraSize();
    }

    private void UpdateAuraSize()
    {
        circleCollider.radius = auraSize;
        if (auraRenderer != null)
        {
            auraRenderer.transform.localScale = new Vector3(auraSize * 2, auraSize * 2, 1);
        }
    }
}
