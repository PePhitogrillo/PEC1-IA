using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RuidoPlayer : MonoBehaviour
{
    public float tiempoParaSumarRuido = 1;
    public float tiempoTranscurrido = 0;
    public bool stopCoroutine = false;
    public float ruidoPlayerMaximo = 100;
    public TMP_Text textoRuido;
    public Image barraRuido;
    public float ruidoPlayerActual;
    public float tiempoParaRestarRuido = 1;
    public Color alerta = Color.green;
    public Color sospecha = Color.red;
    private void Start()
    {
        ruidoPlayerActual = 0;
    }

    private void Update()
    {
        //textoRuido.text = ruidoPlayerActual.ToString();
        if (ruidoPlayerActual < 50)
        {
            barraRuido.color = sospecha;
            Debug.Log("el color de la barra es sospecha");
        }
        else
        {
            barraRuido.color = alerta;
            Debug.Log("el color de la barra es alerta");
        }
        barraRuido.fillAmount = ruidoPlayerActual / ruidoPlayerMaximo;
        

    }
    public void SumarRuido(int cantidadDeRuido)
    {
        ruidoPlayerActual += cantidadDeRuido;
        
        if (ruidoPlayerActual > ruidoPlayerMaximo)
        {
            ruidoPlayerActual = ruidoPlayerMaximo;
        }
    }

    public void RestarRuido(int cantidadDeRuido)
    {
        ruidoPlayerActual -= cantidadDeRuido;
        if (ruidoPlayerActual < 0)
        {
            ruidoPlayerActual = 0;
        }
    }

    public IEnumerator EsperarParaRestarRuido(int cantidadDeRuido)
    {
        yield return new WaitForSeconds(tiempoParaRestarRuido);
        RestarRuido(cantidadDeRuido);
         stopCoroutine = false;
    }

    
}    
