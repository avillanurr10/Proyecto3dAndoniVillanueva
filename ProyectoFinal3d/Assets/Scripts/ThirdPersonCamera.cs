using UnityEngine;

public class ThirdPersonCamera_FortniteStyle : MonoBehaviour
{
    [Header("Referencias")]
    public Transform target; // Jugador

    [Header("Ajustes de cámara")]
    public float distance = 4f;       // Distancia de la cámara al jugador
    public float height = 1.5f;       // Altura respecto al jugador
    public float mouseSensitivity = 2f;
    public float rotationSmoothTime = 0.12f; // suavizado del giro
    public float followSmoothTime = 0.1f;    // suavizado del movimiento

    [Header("Límites verticales")]
    public float minYAngle = -35f;
    public float maxYAngle = 60f;

    private float yaw;   // rotación horizontal
    private float pitch; // rotación vertical
    private Vector3 currentRotation;
    private Vector3 rotationSmoothVelocity;

    void Start()
    {
        // Inicializar mirando hacia adelante
        Vector3 angles = transform.eulerAngles;
        yaw = angles.y;
        pitch = angles.x;

        // Bloquear cursor estilo Fortnite
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        if (!target) return;

        // --- CONTROL DE MOUSE ---
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        yaw += mouseX * mouseSensitivity;
        pitch -= mouseY * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, minYAngle, maxYAngle);

        // --- SUAVIZAR ROTACIÓN ---
        Vector3 targetRotation = new Vector3(pitch, yaw);
        currentRotation = Vector3.SmoothDamp(currentRotation, targetRotation, ref rotationSmoothVelocity, rotationSmoothTime);

        // Aplicar rotación
        transform.eulerAngles = currentRotation;

        // --- POSICIÓN DE LA CÁMARA ---
        Vector3 targetPosition = target.position 
                                 - transform.forward * distance 
                                 + Vector3.up * height;

        transform.position = Vector3.Lerp(transform.position, targetPosition, followSmoothTime);

        // --- OPCIONAL: hacer que el jugador mire donde mira la cámara ---
        // Si tu jugador tiene un modelo (playerModel) que rota, puedes rotarlo aquí:
        // playerModel.rotation = Quaternion.Euler(0, yaw, 0);
    }
}
