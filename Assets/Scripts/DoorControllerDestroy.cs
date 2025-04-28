using UnityEngine;

public class DoorControllerDestroy : MonoBehaviour
{
    [Header("Sprites de la Puerta")]
    [SerializeField] private Sprite doorNormalSprite;    // Estado 0 daño
    [SerializeField] private Sprite doorDamage1Sprite;   // Estado 1 daño
    [SerializeField] private Sprite doorDamage2Sprite;   // Estado 2 daños
    [SerializeField] private Sprite doorBrokenSprite;    // Estado destruido

    [Header("Configuración")]
    [SerializeField] private int maxHealth = 3;          // Disparos necesarios para destruir
    [SerializeField] private float destroyDelay = 1f;    // Tiempo antes de destruir el objeto
    [SerializeField] private bool showDebug = true;      // Mostrar mensajes de debug

    [Header("Efectos")]
    [SerializeField] private AudioClip hitSound;         // Sonido al recibir disparo
    [SerializeField] private AudioClip breakSound;       // Sonido al romperse
    [SerializeField] private GameObject hitEffectPrefab; // Efecto de partículas opcional

    // Referencias privadas
    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;
    private BoxCollider2D doorCollider;
    private int currentHealth;
    private bool isDestroyed = false;

    private void Start()
    {
        // Obtener componentes
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        doorCollider = GetComponent<BoxCollider2D>();

        // Verificar componentes necesarios
        if (spriteRenderer == null)
        {
            Debug.LogError("¡Falta SpriteRenderer en " + gameObject.name + "!");
            return;
        }

        // Añadir AudioSource si no existe
        if (audioSource == null && (hitSound != null || breakSound != null))
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Inicializar
        currentHealth = maxHealth;
        UpdateDoorSprite();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verificar si es una bala usando el componente BulletTrail
        BulletTrail bullet = other.GetComponent<BulletTrail>();

        if (bullet != null && !isDestroyed)
        {
            TakeDamage();
            // No destruimos la bala aquí ya que tu sistema parece manejarla diferente
        }
    }

    private void TakeDamage()
    {
        if (isDestroyed) return;

        currentHealth--;

        // Reproducir sonido de impacto
        PlaySound(hitSound);

        // Mostrar efecto de impacto
        ShowHitEffect();

        // Actualizar sprite según el daño
        UpdateDoorSprite();

        if (showDebug)
        {
            Debug.Log($"Puerta dañada. Salud restante: {currentHealth}");
        }

        // Verificar si debe ser destruida
        if (currentHealth <= 0)
        {
            DestroyDoor();
        }
    }

    private void UpdateDoorSprite()
    {
        if (spriteRenderer == null) return;

        // Asignar sprite según el nivel de daño
        spriteRenderer.sprite = currentHealth switch
        {
            3 => doorNormalSprite,
            2 => doorDamage1Sprite,
            1 => doorDamage2Sprite,
            _ => doorBrokenSprite
        };
    }

    private void DestroyDoor()
    {
        isDestroyed = true;

        // Reproducir sonido de destrucción
        PlaySound(breakSound);

        // Cambiar al sprite de puerta rota
        spriteRenderer.sprite = doorBrokenSprite;

        // Desactivar el collider
        if (doorCollider != null)
        {
            doorCollider.enabled = false;
        }

        // Efecto de destrucción
        ShowDestroyEffect();

        // Destruir el objeto después de un delay
        Destroy(gameObject, destroyDelay);

        if (showDebug)
        {
            Debug.Log("¡Puerta destruida!");
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    private void ShowHitEffect()
    {
        // Efecto visual de impacto
        StartCoroutine(FlashEffect());

        // Instanciar efecto de partículas si existe
        if (hitEffectPrefab != null)
        {
            Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
        }
    }

    private void ShowDestroyEffect()
    {
        // Aquí puedes añadir efectos especiales de destrucción
        // Por ejemplo, partículas, animación, etc.
    }

    private System.Collections.IEnumerator FlashEffect()
    {
        if (spriteRenderer == null) yield break;

        Color originalColor = spriteRenderer.color;
        spriteRenderer.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = originalColor;
    }

    // Visualización en el editor
    private void OnDrawGizmos()
    {
        if (!showDebug) return;

        Gizmos.color = Color.yellow;
        if (doorCollider != null)
        {
            Gizmos.DrawWireCube(transform.position, doorCollider.size);
        }
    }
}