using UnityEngine;
using Pathfinding;
using System.Collections;

[RequireComponent(typeof(AIPath))]
[RequireComponent(typeof(AIDestinationSetter))]
public class EnemyVision : MonoBehaviour
{
    [Header("Vision Settings")]
    public float viewRadius = 5f;
    [Range(0, 360)]
    public float viewAngle = 90f;
    public LayerMask obstructionLayer;

    [Header("References")]
    public Transform player;

    private AIPath aiPath;
    private AIDestinationSetter destSetter;
    private Vector3 originalPosition;
    private Vector3 lastKnownPosition;
    private bool wasSeeing = false;
    private bool investigating = false;
    private bool returningHome = false;
    private Coroutine investigateCoroutine;
    private Coroutine returnCoroutine;

    void Awake()
    {
        aiPath = GetComponent<AIPath>();
        destSetter = GetComponent<AIDestinationSetter>();

        // Store initial position and setup
        originalPosition = transform.position;
        lastKnownPosition = originalPosition;

        // Configure AI components
        destSetter.target = player;
        destSetter.enabled = true;
        aiPath.canMove = false;
    }
    void Start()
    {
        AstarPath.active.Scan();
    }

    void Update()
    {
        if (player == null) return;

        bool sees = CanSeePlayer();

        if (sees)
        {
            // Cancel all pending actions if player is spotted
            StopAllCoroutines();
            ResetStates();

            // Start chase
            destSetter.enabled = true;
            aiPath.canMove = true;
            lastKnownPosition = player.position;
            aiPath.SearchPath();
        }
        else
        {
            // Start investigation if just lost sight
            if (wasSeeing && !investigating && investigateCoroutine == null)
            {
                investigateCoroutine = StartCoroutine(InvestigateAfterDelay());
            }

            // Start return home sequence after investigation
            if (investigating && aiPath.reachedEndOfPath && returnCoroutine == null)
            {
                investigating = false;
                aiPath.canMove = false;
                returnCoroutine = StartCoroutine(ReturnHomeAfterDelay());
            }

            // Stop when reached home
            if (returningHome && aiPath.reachedEndOfPath)
            {
                returningHome = false;
                aiPath.canMove = false;
            }
        }

        wasSeeing = sees;
    }

    private void ResetStates()
    {
        investigating = false;
        returningHome = false;
        investigateCoroutine = null;
        returnCoroutine = null;
    }

    private IEnumerator InvestigateAfterDelay()
    {
        yield return new WaitForSeconds(1f);

        if (!CanSeePlayer())
        {
            investigating = true;
            destSetter.enabled = false;
            aiPath.canMove = true;
            lastKnownPosition = player.position;
            aiPath.destination = lastKnownPosition;
            aiPath.SearchPath();
        }

        investigateCoroutine = null;
    }

    private IEnumerator ReturnHomeAfterDelay()
    {
        yield return new WaitForSeconds(3f);

        if (!CanSeePlayer())
        {
            returningHome = true;
            destSetter.enabled = false;
            aiPath.canMove = true;
            aiPath.destination = originalPosition;
            aiPath.SearchPath();
        }

        returnCoroutine = null;
    }

    public bool CanSeePlayer()
    {
        Vector2 dir = (player.position - transform.position).normalized;
        float dist = Vector2.Distance(transform.position, player.position);
        if (dist > viewRadius) return false;

        Vector2 forward = transform.right; // Adjust to transform.up if sprite faces upward
        if (Vector2.Angle(forward, dir) > viewAngle * 0.5f)
            return false;

        // Offset origin slightly to avoid self-collision
        Vector2 origin = (Vector2)transform.position + forward * 0.1f;
        var hit = Physics2D.Raycast(origin, dir, dist, obstructionLayer);
        Debug.DrawRay(origin, dir * dist, hit.collider == null ? Color.green : Color.red);
        return hit.collider == null;
    }

    void OnDrawGizmosSelected()
    {
        if (player == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, viewRadius);
        Vector3 forward = transform.right;
        Vector3 leftDir = Quaternion.Euler(0, 0, viewAngle / 2) * forward;
        Vector3 rightDir = Quaternion.Euler(0, 0, -viewAngle / 2) * forward;
        Gizmos.DrawLine(transform.position, transform.position + leftDir * viewRadius);
        Gizmos.DrawLine(transform.position, transform.position + rightDir * viewRadius);
    }
}
