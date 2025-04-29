using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCompleted : MonoBehaviour
{

    public Canvas targetCanvas;


    private void Start()
    {

        // confirma que el canva esta desactivado

        if (targetCanvas != null)
            targetCanvas.gameObject.SetActive(false);
        
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Activa el canva si colisiona con el gameobject

        if (other.CompareTag("Player"))
        {
            if (targetCanvas != null)
                targetCanvas.gameObject.SetActive(true);
        
                
            PlayerMovement.instance.canMove = false;
            PlayerMovement.instance.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }


    
}