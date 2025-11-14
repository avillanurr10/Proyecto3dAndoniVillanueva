using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class EnemyAI : MonoBehaviour
{
    [Header("Referencias")]
    public Transform playerTransform;
    public Animator animator;
    public CharacterController controller;
    public EnemyHealth health;

    [Header("Ajustes")]
    public float moveSpeed = 3f;
    public float rotationSpeed = 5f;
    public float attackRange = 2f;
    public float attackCooldown = 1.5f;
    public int damage = 10;
    public float gravity = -9.81f;

    private bool canAttack = true;
    private Vector3 velocity;

    void Start()
    {
        if (controller == null)
            controller = GetComponent<CharacterController>();
        if (animator == null)
            animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (health != null && health.isDead) return;
        if (playerTransform == null) return;

        // Aplicar gravedad
        if (controller.isGrounded && velocity.y < 0)
            velocity.y = -2f;
        else
            velocity.y += gravity * Time.deltaTime;

        float distance = Vector3.Distance(transform.position, playerTransform.position);

        if (distance > attackRange)
            FollowPlayer();
        else
            AttackPlayer();

      
        controller.Move(velocity * Time.deltaTime);
    }

    void FollowPlayer()
    {
        Vector3 direction = (playerTransform.position - transform.position);
        direction.y = 0;
        direction.Normalize();

        controller.Move(direction * moveSpeed * Time.deltaTime);

        if (direction != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        animator.SetFloat("Speed", 1f);
        animator.SetBool("IsAttacking", false);
    }

    void AttackPlayer()
    {
        animator.SetFloat("Speed", 0f);

        if (canAttack)
            StartCoroutine(PerformAttack());
    }

    IEnumerator PerformAttack()
    {
        canAttack = false;
        animator.SetBool("IsAttacking", true);

      
        yield return new WaitForSeconds(0.5f);

        //  Solo aplicar daño si sigue en rango y vivo
        if (Vector3.Distance(transform.position, playerTransform.position) <= attackRange)
        {
            PlayerHealth playerHealth = playerTransform.GetComponentInParent<PlayerHealth>();
            if (playerHealth != null)
                playerHealth.TakeDamage(damage);
        }

        yield return new WaitForSeconds(attackCooldown);

        animator.SetBool("IsAttacking", false);
        canAttack = true;
    }
}
