using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoints : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {


        if (other.CompareTag("Player"))
        {

            PlayerMovement player = other.GetComponent<PlayerMovement>();
            if (player != null)
            {


                player.SetCheckpoint(transform);
            }
        }
    }
}