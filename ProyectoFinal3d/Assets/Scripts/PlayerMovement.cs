using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movimiento")]
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float gravityScale = 2f;

    private CharacterController playerController;
    private Vector3 moveDirection;
    private float yVelocity;

    [Header("Ataque")]
    public float attackCooldown = 0.8f;
    private bool canAttack = true;
    public bool IsAttacking { get; private set; }

    [Header("Estados")]
    public bool isDead = false;
    public bool isHit = false;
    private bool isInvulnerable = false;

    [Header("Referencias")]
    public Camera playerCamera;
    public Transform playerModel;
    public Animator animator;
    public GameObject mesh;

    [Header("Audio")]
    public AudioSource attackAudio;   

    private Vector3 spawnPoint;

    [Header("Animaciones")]
    public float hitAnimDuration = 0.75f;
    public float deathAnimDuration = 1.17f;
    public float respawnDelay = 0.5f;

    void Start()
    {
        playerController = GetComponent<CharacterController>();
        spawnPoint = transform.position;

        if (animator == null && playerModel != null)
            animator = playerModel.GetComponent<Animator>();

        if (mesh == null && playerModel != null)
            mesh = playerModel.GetChild(0).gameObject;
    }

    void Update()
    {
        if (isDead) return;

        if (!IsAttacking && !isHit)
            HandleMovement();

        HandleAttack();
        HandleAnimations();
    }

    void HandleMovement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        Vector3 camForward = playerCamera.transform.forward;
        Vector3 camRight = playerCamera.transform.right;
        camForward.y = 0f;
        camRight.y = 0f;
        camForward.Normalize();
        camRight.Normalize();

        moveDirection = (camForward * moveZ + camRight * moveX).normalized;

        if (playerController.isGrounded)
        {
            yVelocity = -1f;
            if (Input.GetButtonDown("Jump"))
                yVelocity = jumpForce;
        }
        else
        {
            yVelocity += Physics.gravity.y * gravityScale * Time.deltaTime;
        }

        Vector3 move = moveDirection * moveSpeed;
        move.y = yVelocity;
        playerController.Move(move * Time.deltaTime);

        if (moveDirection != Vector3.zero && playerModel != null)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            playerModel.rotation = Quaternion.Lerp(playerModel.rotation, toRotation, Time.deltaTime * 10f);
        }
    }

    void HandleAttack()
    {
        if (animator == null || IsAttacking || isHit || isDead) return;

        if (Input.GetMouseButtonDown(0) && canAttack)
            StartCoroutine(PerformAttack());
    }

    IEnumerator PerformAttack()
    {
        canAttack = false;
        IsAttacking = true;

        animator.SetBool("IsAttacking", true);

        // -------- SONIDO DEL ATAQUE --------
        if (attackAudio != null)
            attackAudio.Play();
        //------------------------------------

        yield return new WaitForSeconds(attackCooldown);

        animator.SetBool("IsAttacking", false);
        IsAttacking = false;
        canAttack = true;
    }

    public void TakeHit()
    {
        if (isDead || isHit || isInvulnerable) return;
        StartCoroutine(HitRoutine());
    }

    private IEnumerator HitRoutine()
    {
        isHit = true;
        isInvulnerable = true;

        animator.SetTrigger("Hit");

        yield return new WaitForSeconds(hitAnimDuration);

        isHit = false;

        yield return new WaitForSeconds(0.1f);
        isInvulnerable = false;
    }

    public void DieAndRespawn()
    {
        if (!isDead)
            StartCoroutine(DeathRoutine());
    }

    private IEnumerator DeathRoutine()
    {
        isDead = true;

        animator.SetTrigger("Death");

        playerController.enabled = false;

        yield return new WaitForSeconds(deathAnimDuration + respawnDelay);

        transform.position = spawnPoint;
        playerController.enabled = true;

        isDead = false;
    }

    void HandleAnimations()
    {
        if (animator == null) return;

        Vector3 horizontalVelocity = playerController.velocity;
        horizontalVelocity.y = 0;
        float speed = horizontalVelocity.magnitude / Mathf.Max(0.01f, moveSpeed);

        animator.SetFloat("Speed", speed);
        animator.SetBool("IsJumping", !playerController.isGrounded);
    }
}
