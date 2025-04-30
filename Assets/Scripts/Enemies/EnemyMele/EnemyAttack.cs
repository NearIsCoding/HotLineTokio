using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EnemyAttack : MonoBehaviour
{
    [Header("Ajustes de ataque")]
    public Transform player;             // Arrástralo en el Inspector
    public float attackRange = 1.2f;     // Distancia a la que puede golpear
    public float damage = 20f;           // Daño por golpe
    public float attackCooldown = 1f;    // Segundos entre ataques

    private LifeManager playerLife;
    private float lastAttackTime = 0f;

    void Start()
    {
        if (player == null)
        {
            Debug.LogError($"[{name}] No hay referencia al Transform del jugador.");
            enabled = false;
            return;
        }

        playerLife = player.GetComponent<LifeManager>();
        if (playerLife == null)
        {
            Debug.LogError($"[{name}] El jugador no tiene un LifeManager.");
            enabled = false;
        }
    }

    void Update()
    {
        if (playerLife == null) return;

        // Calcula distancia 2D
        float dist = Vector2.Distance(transform.position, player.position);

        // ¿Estamos lo suficientemente cerca y recarga cumplida?
        if (dist <= attackRange && Time.time >= lastAttackTime + attackCooldown)
        {
            DoAttack();
            lastAttackTime = Time.time;
        }
    }

    void DoAttack()
    {
        // Aquí podrías disparar una animación: animator.SetTrigger("Attack");
        // Debug.Log($"[{name}] Ataque: infligiendo {damage} de daño al jugador.");
        playerLife.TakeDamage(damage);
    }

    // Opcional: dibujar el rango de ataque en el Scene View
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
