using UnityEngine;

public class SwordHitbox : MonoBehaviour
{
    public int damage = 1;
    private PlayerMovement player;

    private void Start()
    {
        // Busca el PlayerMovement automáticamente
        player = GetComponentInParent<PlayerMovement>();

        if (player == null)
            Debug.LogError("SwordHitbox no encontró PlayerMovement en los padres!");
    }

    private void OnTriggerEnter(Collider other)
    {
        // Ahora usamos la propiedad correcta
        if (!player.IsAttacking) return;

        EnemyHealth enemy = other.GetComponent<EnemyHealth>();

        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
    }
}
