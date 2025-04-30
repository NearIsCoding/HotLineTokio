using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class BulletDamage : MonoBehaviour
{
    [Header("Damage Settings")]
    public float damage = 1f;

    [Header("Shooter (will be ignored)")]
    public GameObject shooter;

    [Header("Optional Effects")]
    public Animator cameraAnimator;

    void Awake()
    {
        Debug.Log($"[BulletDamage] Awake on {gameObject.name}");
    }

    void OnEnable()
    {
        Debug.Log($"[BulletDamage] OnEnable on {gameObject.name}");
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        HandleHit(col);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        HandleHit(col.collider);
    }

    private void HandleHit(Collider2D col)
    {
        // 1) Ignore the shooter itself
        if (shooter != null && col.gameObject == shooter)
            return;

        // 2) Only process hits on Player or Enemy
        if (!col.CompareTag("Player") && !col.CompareTag("Enemy"))
            return;

        Debug.Log($"[BulletDamage] Hit '{col.name}' (tag: {col.tag})");

        // 3) Camera shake if we hit the player
        if (col.CompareTag("Player") && cameraAnimator != null)
        {
            Debug.Log("[BulletDamage] Triggering camera shake");
            cameraAnimator.SetTrigger("CamHit");
        }

        // 4) Apply damage
        if (col.TryGetComponent<LifeManager>(out var lm))
        {
            Debug.Log($"[BulletDamage] Dealing {damage} damage to Player");
            lm.TakeDamage(damage);
        }
        else if (col.TryGetComponent<EnemyHealth>(out var eh))
        {
            Debug.Log($"[BulletDamage] Dealing {damage} damage to Enemy '{col.name}'");
            eh.TakeDamage(damage);
        }
        else
        {
            Debug.LogWarning($"[BulletDamage] '{col.name}' has no LifeManager or EnemyHealth component");
        }

        // 5) Destroy this bullet
        Debug.Log("[BulletDamage] Destroying bullet");
        Destroy(gameObject);
    }
}
