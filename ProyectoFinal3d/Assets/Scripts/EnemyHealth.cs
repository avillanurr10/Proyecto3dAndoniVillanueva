using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    [Header("Vida")]
    public int maxHealth = 5;
    private int currentHealth;

    [Header("Animaciones")]
    public Animator animator;
    public float hitDuration = 0.75f;
    public float deathDuration = 1.17f;

    [Header("Drop de Monedas")]
    public GameObject monedaPrefab;
    public int minCoins = 1;
    public int maxCoins = 3;

    [HideInInspector] public bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
        if (animator == null)
            animator = GetComponentInChildren<Animator>();
    }

    public void TakeDamage(int dmg)
    {
        if (isDead) return;

        currentHealth -= dmg;

       
        animator.SetTrigger("Hit");
        StartCoroutine(HitRoutine());

        if (currentHealth <= 0)
            Die();
    }

    IEnumerator HitRoutine()
    {
      
        yield return new WaitForSeconds(hitDuration);
    }

    void Die()
    {
        isDead = true;
        animator.SetTrigger("Death");
        StartCoroutine(DeathRoutine());
    }

    IEnumerator DeathRoutine()
    {
      
        yield return new WaitForSeconds(deathDuration);

        DropCoins();
        Destroy(gameObject);
    }

    void DropCoins()
    {
        if (monedaPrefab == null) return;

        int amount = Random.Range(minCoins, maxCoins + 1);
        for (int i = 0; i < amount; i++)
        {
            GameObject coin = Instantiate(monedaPrefab, transform.position + Vector3.up * 1f, Quaternion.identity);
            Rigidbody rb = coin.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 force = new Vector3(
                    Random.Range(-2f, 2f),
                    Random.Range(2f, 4f),
                    Random.Range(-2f, 2f)
                );
                rb.AddForce(force, ForceMode.Impulse);
            }
        }
    }
}
