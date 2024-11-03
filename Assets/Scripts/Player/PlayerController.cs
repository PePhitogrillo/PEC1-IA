using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /* Necesitamos una funcion para el movimiento de los ejes horizontal y vertical(x y z )
     variable de velocidad decimal para mejor control de la misma, un componente rigidbody al que le añadiremos esa velocidad y necesitamos un vector3 al que le vamos 
    a asignar las variables float que obtenemos de los input.getAxis.Horizontal y vertical. */

    public float moveHorizontal;
    public float moveVertical;
    public float velocidadBase = 5f;
    public float velocidadPlayerActual;
    public Rigidbody player;
    public Transform camaraTransform;
    public RuidoPlayer ruidoPlayer;
    public void PlayerMovement()
    {
      
         moveHorizontal = Input.GetAxis("Horizontal");
         moveVertical = Input.GetAxis("Vertical");
        
        Vector3 forward = camaraTransform.forward;
        Vector3 right = camaraTransform.right;

        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();
        

        Vector3 movimiento = (forward * moveVertical + right * moveHorizontal).normalized;
        
        
        
        
        player.MovePosition(transform.position + movimiento * velocidadPlayerActual * Time.fixedDeltaTime);    
        
    }
    public void FixedUpdate()
    {
        PlayerMovement();
        Correr();


    }

    public void Correr()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            velocidadPlayerActual = velocidadBase * 2;
            if (moveHorizontal != 0 || moveVertical != 0)
            {

                ruidoPlayer.tiempoTranscurrido += Time.deltaTime;
                if (ruidoPlayer.tiempoTranscurrido > ruidoPlayer.tiempoParaSumarRuido) // EL IF ES UNA COMPROBACION HUEVON.
                {
                    ruidoPlayer.SumarRuido(5);
                    ruidoPlayer.tiempoTranscurrido = 0;
                }
                
                    

            }
        }
        else
        {
            velocidadPlayerActual = velocidadBase;
            if (!ruidoPlayer.stopCoroutine)
            {
                ruidoPlayer.stopCoroutine = true;
                ruidoPlayer.StartCoroutine(ruidoPlayer.EsperarParaRestarRuido(5));
            }
        }
    }
 }
