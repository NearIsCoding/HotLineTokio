using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class daño_jugador : MonoBehaviour
{

    //public float daño; → Define cuánto daño recibe el jugador.
    // public Transform player; → Referencia al transform del jugador.
    // public Animator animator; → Controla la animación del daño.
    // private bool DañoGradual = true; → Controla si el daño se puede aplicar en ese momento.


    public float daño;
    public Transform player;
    public Animator Camara;
 
    private bool DañoGradual = true;
   

    // Se ejecuta cuando un objeto permanece dentro del área de colisión.
    // Verifica si el objeto tiene la etiqueta "Player".
    // Si DañoGradual es true, inicia la corrutina WaitForDamage(colision).

    private void OnTriggerStay2D(Collider2D colision)
    {
        if (colision.CompareTag("Player") && DañoGradual)
        {
            Camara.SetTrigger("CamHit");
            StartCoroutine(WaitForDamage(colision));
            
        }
    }


    // Bloquea el daño temporalmente: DañoGradual = false;.
    // Ignora colisiones entre capas: Physics2D.IgnoreLayerCollision(3, 6, true);.
    // Modifica la animación: animator.SetBool("IsHit2", false);.
    // Reduce la vida del jugador: colision.GetComponent<Barra_de_vida>().vidaAct -= daño;.
    // Espera 3 segundos antes de permitir que el daño ocurra nuevamente.
    // Restablece la animación y la colisión entre capas.
    // Vuelve a habilitar el daño gradual.


    IEnumerator WaitForDamage(Collider2D colision)
    {
        DañoGradual = false;
        Physics2D.IgnoreLayerCollision(3, 6, true);

       
        colision.GetComponent<LifeManager2>().vidaAct -= daño;



        yield return new WaitForSeconds(2f); // Ahora espera 3 segundos antes de hacer daño de nuevo
        Physics2D.IgnoreLayerCollision(3, 6, false);
        
        DañoGradual = true;
    }
}