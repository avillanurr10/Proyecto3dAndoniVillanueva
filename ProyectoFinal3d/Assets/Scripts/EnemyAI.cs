using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class EnemyAI : MonoBehaviour
{
    [Header("Referencias")]
    public Transform playerHitBox;   // HitBox del jugador
    public Animator animator;
    public CharacterController controller;

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
        if (playerHitBox == null) return;

        // Aplicar gravedad
        if (controller.isGrounded && velocity.y < 0)
            velocity.y = -2f;
        else
            velocity.y += gravity * Time.deltaTime;

        float distance = Vector3.Distance(transform.position, playerHitBox.position);

        if (distance > attackRange)
            FollowPlayer();
        else
            AttackPlayer();

        // Aplicar movimiento final con gravedad
        controller.Move(velocity * Time.deltaTime);
    }

    void FollowPlayer()
    {
        Vector3 direction = (playerHitBox.position - transform.position);
        direction.y = 0;
        direction.Normalize();

        if (canAttack) // solo mueve si puede atacar (evita "pegado")
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

        // Esperar la mitad de la animación para aplicar daño
        yield return new WaitForSeconds(0.5f);

        // Comprobar distancia real antes de aplicar daño
        if (Vector3.Distance(transform.position, playerHitBox.position) <= attackRange)
        {
            PlayerHealth health = playerHitBox.GetComponentInParent<PlayerHealth>();
            if (health != null)
                health.TakeDamage(damage);
        }

        // Esperar cooldown antes del siguiente ataque
        yield return new WaitForSeconds(attackCooldown);

        animator.SetBool("IsAttacking", false);
        canAttack = true;
    }
}
