using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    private PlayerMovement movement;

    // === GETTERS PARA LA BARRA DE VIDA ===
    public int VidaActual => currentHealth;
    public int VidaMaxima => maxHealth;
    // =====================================

    void Start()
    {
        currentHealth = maxHealth;
        movement = GetComponent<PlayerMovement>();
    }

    public void TakeDamage(int damage)
    {
        if (movement == null || movement.isDead) return;

        currentHealth -= damage;
        Debug.Log($"Jugador recibe {damage} daño. Vida: {currentHealth}");

        if (currentHealth > 0)
        {
            movement.TakeHit();
        }
        else
        {
            currentHealth = 0;
            movement.DieAndRespawn();
            StartCoroutine(ResetHealthAfterRespawn());
        }
    }

    private IEnumerator ResetHealthAfterRespawn()
    {
        // Esperar a que termine la animación de muerte + respawn
        yield return new WaitForSeconds(movement.deathAnimDuration + 0.6f);
        currentHealth = maxHealth;
    }
}
