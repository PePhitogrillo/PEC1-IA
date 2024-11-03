using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoCamara : MonoBehaviour
{
    public float sensRaton = 0f;
    public float rotacionX = 0f;
    public Transform player;
    public float valorMinimoLimitante;
    public float valorMaximoLimitante;

   


    /*Necesitamos dos scripts 1 para la camara y otro para la rotacion del player, haremos una variable float de sens del raton, añadiremos una referencia al tranform del jugador,
     y haremos una variable float ( rotacion x y la igualaremos a 0 y la usaremos para controlar la rotacion en x)
    vamos a fijar el raton en el centro de la pantalla con ( Cursor.lockState = CursorLockMode.Locked;) y para anularlo Cursor.lockState = CursorLockMode.Unlocked;

    vamos a hacer una funcion para controlar el movimiento de la camara 
    a la funcion le vamos a llamar 2 floats mouse x y mouse y, y los igualaremos  input.getAxis("Mouse X") y input.GetAxis("Mouse Y")

    estos floats los multiplicaremos por los floats de sens del mouse y por time.deltatime y lo llamaremos en el update ya que no esta el rigidbody y no tenemos fisicas en medio

    dentro de la funcion de control de camara cogeremos la variable de rotacion x le vamos a restar la variable local mouse Y, y luego en la siguiente linea igualaremos de nuevo
    la variable de rotacion x a una funcion matematica de unity, (Mathf.Clamp) se utiliza para limitar un valor con un rango limitado

    aplicaremos la variable  rotacion x se la aplicaremos  al transform de la camara y al del jugador.
    */

    public void ControlCamara()
    {
        //Para obtener el movimiento del raton.
        float mouseX = Input.GetAxis("Mouse X") * sensRaton * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensRaton * Time.deltaTime;
        //Para darle valor a la rotacion y limitarla
        rotacionX -= mouseY;
        rotacionX = Mathf.Clamp(rotacionX, valorMinimoLimitante, valorMaximoLimitante);
        //Aplicaremos la rotacion a la camara y al player
        transform.localRotation = Quaternion.Euler(rotacionX, 0f, 0f);
        player.Rotate(Vector3.up * mouseX );
    }
    
    public void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
       
    }

    public void Update()
    {
        ControlCamara();
    }
}


