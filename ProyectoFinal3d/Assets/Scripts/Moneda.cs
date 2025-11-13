using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moneda : MonoBehaviour
{
    public Stats  _stats;
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _stats.monedas++;
            Destroy(gameObject);
        }
    }
}
