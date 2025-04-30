using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public BoxCollider2D collider;
    // Referencias a los sprites
    [Header("Sprites")]
    [SerializeField] private Sprite doorClosedSprite;
    [SerializeField] private Sprite doorOpenSprite;

    // Referencias a componentes
    private SpriteRenderer spriteRenderer;
    private bool playerInRange = false;
    private bool isOpen = false;

    // Debug para ver si el jugador está en rango
    [Header("Debug")]
    [SerializeField] private bool showDebug = true;

    void Start()
    {
        collider.enabled = true;
        // Obtener el componente SpriteRenderer
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Verificar que tenemos todos los componentes necesarios
        if (spriteRenderer == null)
        {
            Debug.LogError("¡No se encontró el SpriteRenderer en " + gameObject.name + "!");
            return;
        }

        if (doorClosedSprite == null || doorOpenSprite == null)
        {
            Debug.LogError("¡Faltan sprites! Por favor asigna los sprites en el inspector.");
            return;
        }

        // Establecer el sprite inicial
        spriteRenderer.sprite = doorClosedSprite;
    }

    void Update()
    {
        // Si el jugador está en rango y presiona F
        if (playerInRange && Input.GetKeyDown(KeyCode.Space))
        {
            ToggleDoor();
            if (showDebug)
            {
                Debug.Log("Puerta " + (isOpen ? "abierta" : "cerrada"));
            }
        }
    }

    private void ToggleDoor()
    {
        isOpen = !isOpen;
        collider.enabled = !collider.enabled;
        spriteRenderer.sprite = isOpen ? doorOpenSprite : doorClosedSprite;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            if (showDebug)
            {
                Debug.Log("Jugador entró en rango");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            if (showDebug)
            {
                Debug.Log("Jugador salió del rango");
            }
        }
    }
}