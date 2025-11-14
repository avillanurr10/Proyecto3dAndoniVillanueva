using UnityEngine;
using UnityEngine.UI;

public class BarraVida : MonoBehaviour
{
    public PlayerHealth playerHealth;     
    public RawImage barraVida;            

    private float vidaMaxima;
    private RectTransform barraRect;
    private float anchoInicial;

    void Start()
    {
        vidaMaxima = playerHealth.VidaMaxima;

        barraRect = barraVida.GetComponent<RectTransform>();
        anchoInicial = barraRect.sizeDelta.x;
    }

    void Update()
    {
        float porcentaje = (float)playerHealth.VidaActual / vidaMaxima;

        barraRect.sizeDelta = new Vector2(
            anchoInicial * Mathf.Clamp01(porcentaje),
            barraRect.sizeDelta.y
        );
    }
}
