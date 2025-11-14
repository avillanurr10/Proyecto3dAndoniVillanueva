using System.Collections;
using UnityEngine;

public class Moneda : MonoBehaviour
{
    public Stats _stats;
    public float velocidadGiro = 2f;

    [Header("Audio")]
    public AudioClip sonidoMoneda;   // sonido que dura 1 segundo
    public float volumen = 1f;

    private bool recogida = false;

    void Update()
    {
        transform.Rotate(0, velocidadGiro * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (recogida) return; // evita doble recogida

        if (other.CompareTag("Player"))
        {
            recogida = true;

            _stats.monedas++;

            // Reproduce el sonido en la posición de la moneda
            AudioSource.PlayClipAtPoint(sonidoMoneda, transform.position, volumen);

            // Destruye la moneda después de un pequeño delay
            Destroy(gameObject, 0.05f);
        }
    }
}
