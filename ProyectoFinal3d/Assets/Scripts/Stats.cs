using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Stats : MonoBehaviour
{
    public int monedas = 0;
    public int vidas = 100;

    [Header("Textos")]
    public TextMeshProUGUI monedasTxt;
    
    public void Update()
    {
       monedasTxt.text = " "+ monedas.ToString(); 
    }
}
