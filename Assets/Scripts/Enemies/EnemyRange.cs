using UnityEngine;

[RequireComponent(typeof(EnemyVision))]
public class EnemyRange : MonoBehaviour
{
    [Header("Ranged Attack Settings")]
    [Tooltip("Must be a prefab from Project window, NOT a scene instance")]
    public GameObject bulletTrailPrefab;
    public Transform firePoint;
    public float shootCooldown = 1f;
    public float weaponRange = 10f;

    [Header("Accuracy Settings")]
    [Range(0f, 1f)] public float accuracy = 0.8f;
    public float maxMissAngle = 15f;

    [Header("Optional Effects")]
    public Animator muzzleAnimator;

    private EnemyVision vision;
    private float nextShootTime;

    void Awake()
    {
        vision = GetComponent<EnemyVision>();
        ValidateReferences();
    }

    private void ValidateReferences()
    {
        if (bulletTrailPrefab == null)
            Debug.LogError($"[EnemyRange] Missing bulletTrailPrefab on {gameObject.name}!");
        if (firePoint == null)
            Debug.LogError($"[EnemyRange] Missing firePoint on {gameObject.name}!");
    }

    void Update()
    {
        if (vision.CanSeePlayer() && Time.time >= nextShootTime)
        {
            Shoot();
            nextShootTime = Time.time + shootCooldown;
        }
    }

    void Shoot()
    {
        // Protect against missing prefab
        if (bulletTrailPrefab == null)
        {
            Debug.LogWarning("[EnemyRange] Skipping shot - bulletTrailPrefab is null");
            return;
        }

        // Optional muzzle flash
        if (muzzleAnimator != null)
            muzzleAnimator.SetTrigger("Shoot");

        // Calculate shooting direction
        Vector2 dirToPlayer = (vision.player.position - firePoint.position).normalized;
        Vector2 shootDir = CalculateShootDirection(dirToPlayer);

        // Create bullet
        GameObject bullet = Instantiate(bulletTrailPrefab, firePoint.position, Quaternion.identity);

        // Setup bullet components
        SetupBullet(bullet, shootDir);
    }

    private Vector2 CalculateShootDirection(Vector2 dirToPlayer)
    {
        if (Random.value <= accuracy)
            return dirToPlayer;

        float missAngle = Random.Range(-maxMissAngle, maxMissAngle);
        return Quaternion.Euler(0, 0, missAngle) * dirToPlayer;
    }

    private void SetupBullet(GameObject bullet, Vector2 shootDir)
    {
        // Set shooter reference
        if (bullet.TryGetComponent<BulletDamage>(out var bd))
            bd.shooter = gameObject;

        // Ignore collision with shooter
        if (bullet.TryGetComponent<Collider2D>(out var bc) &&
            TryGetComponent<Collider2D>(out var myCol))
        {
            Physics2D.IgnoreCollision(bc, myCol, true);
        }

        // Initialize bullet movement
        if (bullet.TryGetComponent<BulletTrail>(out var trail))
        {
            Vector3 targetPos = firePoint.position + (Vector3)(shootDir * weaponRange);
            trail.SetTargetPosition(targetPos);
        }
    }
}
