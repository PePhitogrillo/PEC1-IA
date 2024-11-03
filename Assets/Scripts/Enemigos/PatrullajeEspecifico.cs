using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class PatrullajeEspecifico : MonoBehaviour
{
    public Transform[] posicionesPatrullaje;
    public int indicePosiciones = 0;
    public NavMeshAgent navMeshAgent;
    public float distanciaHastaLaPosicion = 0.5f;
    public float distanciaEmpezarShoot = 5f;
    public float distanciaStopFollow = 20f;
    public float distanciaHastaElPlayer;
    public Transform player;
    public bool enemigoDetectado = false;
    public RuidoPlayer ruidoPlayer;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        navMeshAgent.autoBraking = false;

    }
    private void Update()
    {

        //Esto es el calculo de la distancia que hay hasta el jugador.
        distanciaHastaElPlayer = Vector3.Distance(transform.position, player.position);

        //Quiero que el enemigo este detectado cuando la distancia entre el jugador y el bot sea inferior a la distanciaStopFollow y que si el ruido del player es superior al umbral de deteccion del bot, el bot se 
        //gire a disparar.
        // Si la distancia hasta el jugador es inferior o igual a la distancia de stop follow y el ruido del jugador es superior o igual a 50, entonces :

        if (distanciaHastaElPlayer <= distanciaStopFollow && ruidoPlayer.ruidoPlayerMaximo >= 50f)
        {
            // Pondremos enemigo detectado en true.
            enemigoDetectado = true;
        }
        
        //Si la distancia hasta el player es superior o igual distancia stop follow enemigo detectado sera false.
        if (distanciaHastaElPlayer>= distanciaStopFollow) 
        {
            enemigoDetectado = false;
        }
                
        //Si el enemigo detectado es true va a pasar esto:
        if (enemigoDetectado == true)
        {
           //El enemigo se empezara a mover hacia la posicion del jugador. 
            navMeshAgent.isStopped = false;
            navMeshAgent.destination = player.position;
            
            //Cuando la distancia hasta el player sea inferior a la distancia de disparo, entonces:
            if (distanciaHastaElPlayer <= distanciaEmpezarShoot) 
            {
                //Entonces el enemigo se parara y empezara a disparar.
                navMeshAgent.isStopped = true;
                StartShoot();
            }
            else //Si la distancia hasta el player es mayor a distancia empezar shoot el enemigo volvera a moverse.
            {
                navMeshAgent.isStopped = false;
            }

        }
        else //Si el enemigo detectado es false, entonces:
        { 
            // Cuando no tenga nada pendiente volvera a hacer su patrulla.
            if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < distanciaHastaLaPosicion)
            {
                GoToNextPosition();
            }
        }
    }
    public void GoToNextPosition()
    {
        if (posicionesPatrullaje.Length == 0)
        {
            Debug.Log("No hay posiciones a las que ir");
            return;
        }
       
        navMeshAgent.destination = posicionesPatrullaje[indicePosiciones].position;
        indicePosiciones = (indicePosiciones + 1) % posicionesPatrullaje.Length; // El % se utiliza para hacer una division y usar el resto en este caso para sacar la siguiente posicion del array.
        /// destPoint: 
        ///     Esta variable almacena el índice del próximo punto de destino en el array de puntos de patrulla.
        /// (destPoint + 1): 
        ///     Aquí, incrementamos destPoint en 1. 
        ///     Esto significa que después de llegar a un punto, el sistema se prepara para moverse hacia el siguiente punto en el array.
        /// % points.Length: 
        ///     El operador módulo (%) se utiliza para asegurar que el valor de destPoint se reinicie a 0 cuando exceda el número de elementos en el array (es decir, points.Length). 
        ///     Esto crea un ciclo continuo de patrullaje. 
        ///     Por ejemplo, si hay 3 puntos y destPoint inicialmente es 2, la próxima vez que esta línea se ejecute, (2 + 1) % 3 resultará en 0, reiniciando así el ciclo de patrullaje al primer punto.
        ///
        ///         Funcionamiento del Operador Módulo
        ///             El operador módulo (%) es clave para mantener el índice dentro de los límites del array. 
        ///             Funciona devolviendo el resto de una división, y en este contexto, asegura que el índice destPoint siempre sea un valor válido dentro del rango de índices del array points. 
        ///             Veamos cómo funciona paso a paso con un ejemplo:
        ///                 Supongamos que tienes un array points con 3 elementos (índices 0, 1, y 2).
        ///                     Caso 1: destPoint está en 0.
        ///                         (destPoint + 1) % points.Length = (0 + 1) % 3 = 1 % 3 = 1.
        ///                     Caso 2: destPoint está en 1.
        ///                         (destPoint + 1) % points.Length = (1 + 1) % 3 = 2 % 3 = 2.
        ///                     Caso 3: destPoint está en 2 (último índice).
        ///                         (destPoint + 1) % points.Length = (2 + 1) % 3 = 3 % 3 = 0.




    }
    public void StartShoot()
    {
        Debug.Log(" El enemigo esta disparando");
    }

    
}
