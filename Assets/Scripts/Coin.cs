using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour

{


    public ScoreManager monedaPuntajeUI;

    private Collider2D colisionador;
    // Al colisionar con tag player, se añadira 1 a puntaje

    private void Start()
    {
       
        colisionador = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D colision)
    {
        if (colision.CompareTag("Player"))
        {
            colisionador.enabled = false;

            if (Points.Instance != null)
            {
                Points.Instance.SumPuntos(1);
            }


            MonedaPuntaje Moneda_Puntaje = colision.GetComponent<MonedaPuntaje>();

            if (monedaPuntajeUI != null)
            {
                monedaPuntajeUI.AgregarMoneda(0.5f);
            }



            StartCoroutine(Destruir());
        }
    }

    private IEnumerator Destruir()
    {
        yield return new WaitForSeconds(0.1f); // Espera 1 segundo
        Destroy(gameObject);
    }
}
