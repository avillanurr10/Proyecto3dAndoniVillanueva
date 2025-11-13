using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    public bool isGrounded = false;

    void OnTriggerEnter(Collider other)
    {
        isGrounded = true;
    }

    void OnTriggerExit(Collider other)
    {
        isGrounded = false;
    }
}
