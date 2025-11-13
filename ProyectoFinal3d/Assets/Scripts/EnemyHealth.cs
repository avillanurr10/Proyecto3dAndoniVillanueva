using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 5;
    int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int dmg)
    {
        currentHealth -= dmg;

        Debug.Log("Enemy hit! Health left: " + currentHealth);

        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        // aquí pones tu animación de muerte o lo que quieras
        Destroy(gameObject);
    }
}
