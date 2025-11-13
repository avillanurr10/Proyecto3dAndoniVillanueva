using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movimiento")]
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float gravityScale = 2f;

    private Vector3 moveDirection;
    private CharacterController playerController;

    [Header("Referencias")]
    public Camera playerCamera;
    public Transform playerModel; // Arrastra tu Hero_Ice aquí
    public Animator animator;     // Arrastra el Animator de Hero_Ice aquí (opcional, si no, se busca automáticamente)

    private float yVelocity;

    void Start()
    {
        playerController = GetComponent<CharacterController>();

        // Busca Animator automáticamente si no está asignado
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }

        // Si playerModel no está asignado, usar el Transform del Animator
        if (playerModel == null && animator != null)
        {
            playerModel = animator.transform;
        }
    }

    void FixedUpdate()
    {
        // --- MOVIMIENTO ---
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        // Movimiento relativo a la cámara
        Vector3 camForward = playerCamera.transform.forward;
        Vector3 camRight = playerCamera.transform.right;
        camForward.y = 0f;
        camRight.y = 0f;
        camForward.Normalize();
        camRight.Normalize();

        moveDirection = (camForward * moveZ + camRight * moveX);
        moveDirection.Normalize();

        // --- SALTO Y GRAVEDAD ---
        if (playerController.isGrounded)
        {
            yVelocity = -1f;
            if (Input.GetButtonDown("Jump"))
            {
                yVelocity = jumpForce;
            }
        }
        else
        {
            yVelocity += Physics.gravity.y * gravityScale * Time.deltaTime;
        }

        Vector3 move = moveDirection * moveSpeed;
        move.y = yVelocity;

        playerController.Move(move * Time.deltaTime);

        // --- ROTACIÓN DEL MODELO ---
        if (moveDirection != Vector3.zero && playerModel != null)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            playerModel.rotation = Quaternion.Lerp(playerModel.rotation, toRotation, Time.deltaTime * 10f);
        }

        // --- ANIMACIONES ---
        HandleAnimations();
    }

    void HandleAnimations()
    {
        if (animator == null) return;

        // Velocidad horizontal real
        Vector3 horizontalVelocity = playerController.velocity;
        horizontalVelocity.y = 0;

        float speed = horizontalVelocity.magnitude / moveSpeed;
        animator.SetFloat("Speed", speed);

        // Salto
        bool isJumping = !playerController.isGrounded || yVelocity > 0.1f;
        animator.SetBool("IsJumping", isJumping);
    }
}
