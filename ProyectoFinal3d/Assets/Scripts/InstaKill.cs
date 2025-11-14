using UnityEngine;

public class InstantKill : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerHealth player = other.GetComponent<PlayerHealth>();

        if (player != null)
        {
            // Mata al jugador directamente
            player.TakeDamage(player.maxHealth);
        }
    }
}
