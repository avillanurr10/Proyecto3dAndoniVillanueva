using UnityEngine;

public class SwordHitbox : MonoBehaviour
{
    public int damage = 1;
    public PlayerMovement player;

    private void OnTriggerEnter(Collider other)
    {
        if (!player.IsAttacking) return;

        EnemyHealth enemy = other.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
    }
}
