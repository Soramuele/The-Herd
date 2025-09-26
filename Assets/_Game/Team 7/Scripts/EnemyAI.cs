using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public float circleRadius = 8f;     // Distance to maintain from the player
    public float moveSpeed = 3.5f;      // Agent speed
    public float circleSpeed = 2f;      // How fast it circles around
    public float directionSwitchInterval = 5f; // Average time between switches

    private NavMeshAgent agent;
    private Transform player;
    private float angleOffset;
    private int circleDirection; // 1 = clockwise, -1 = counterclockwise
    private float nextSwitchTime;
    private bool isSettled = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;

        // Find player by tag
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogError("EnemyAI: No object with tag 'Player' found!");
            return;
        }

        // Compute the nearest point on the stalking radius from current position
        Vector3 toEnemy = (transform.position - player.position).normalized;
        Vector3 nearestPoint = player.position + toEnemy * circleRadius;

        // Move agent towards that nearest point
        if (NavMesh.SamplePosition(nearestPoint, out NavMeshHit hit, 2f, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }

        // Angle offset is based on where the enemy currently is relative to the player
        angleOffset = Mathf.Atan2(toEnemy.z, toEnemy.x) * Mathf.Rad2Deg;

        // Pick a random starting direction
        circleDirection = Random.value > 0.5f ? 1 : -1;

        // Schedule first direction switch
        ScheduleNextDirectionSwitch();
    }

    void Update()
    {
        if (player == null) return;

        if (!isSettled)
        {
            // Check if we are close enough to the radius to start circling
            float distToPlayer = Vector3.Distance(transform.position, player.position);
            if (Mathf.Abs(distToPlayer - circleRadius) < 0.5f) // tolerance
            {
                isSettled = true;
            }
            else
            {
                LookAtPlayer();
                return;
            }
        }

        // Circling logic
        if (Time.time >= nextSwitchTime)
        {
            circleDirection *= -1; // flip direction
            ScheduleNextDirectionSwitch();
        }

        angleOffset += circleSpeed * circleDirection * Time.deltaTime;

        float radians = angleOffset * Mathf.Deg2Rad;
        Vector3 offset = new Vector3(Mathf.Cos(radians), 0, Mathf.Sin(radians)) * circleRadius;
        Vector3 targetPos = player.position + offset;

        if (NavMesh.SamplePosition(targetPos, out NavMeshHit hit, 2f, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }

        LookAtPlayer();
    }

    private void LookAtPlayer()
    {
        Vector3 lookDir = (player.position - transform.position).normalized;
        lookDir.y = 0;
        if (lookDir != Vector3.zero)
        {
            Quaternion targetRot = Quaternion.LookRotation(lookDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * 5f);
        }
    }

    private void ScheduleNextDirectionSwitch()
    {
        float randomFactor = Random.Range(0.5f, 1.5f);
        nextSwitchTime = Time.time + directionSwitchInterval * randomFactor;
    }
}
