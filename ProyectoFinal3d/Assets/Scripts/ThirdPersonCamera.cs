using UnityEngine;

public class ThirdPersonCamera_FortniteStyle : MonoBehaviour
{
    [Header("Referencias")]
    public Transform target; // Jugador

    [Header("Ajustes de cámara")]
    public float distance = 4f;          // Distancia de la cámara al jugador
    public float height = 1.5f;          // Altura respecto al jugador
    public float mouseSensitivity = 2f;  
    public float rotationSmoothTime = 0.12f; 
    public float followSmoothTime = 0.08f;    // Más rápido y suave

    [Header("Límites verticales")]
    public float minYAngle = -35f;
    public float maxYAngle = 60f;

    private float yaw;
    private float pitch;
    private Vector3 currentRotation;
    private Vector3 rotationSmoothVelocity;
    private Vector3 currentPositionVelocity;

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        yaw = angles.y;
        pitch = angles.x;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        if (!target) return;

        // --- CONTROL DEL MOUSE ---
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        yaw += mouseX * mouseSensitivity;
        pitch -= mouseY * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, minYAngle, maxYAngle);

        // --- SUAVIZAR ROTACIÓN ---
        Vector3 targetRotation = new Vector3(pitch, yaw);
        currentRotation = Vector3.SmoothDamp(currentRotation, targetRotation, ref rotationSmoothVelocity, rotationSmoothTime);
        transform.eulerAngles = currentRotation;

        // --- POSICIÓN DE LA CÁMARA SUAVIZADA ---
        Vector3 desiredPosition = target.position - transform.forward * distance + Vector3.up * height;
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref currentPositionVelocity, followSmoothTime);

        // --- OPCIONAL: ROTO EL PLAYER CON LA CÁMARA ---
        // target.rotation = Quaternion.Euler(0, yaw, 0); // Comentado si no quieres que el player rote automáticamente
    }
}
