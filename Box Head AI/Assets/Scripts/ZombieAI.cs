using UnityEngine;

public class ZombieAI : MonoBehaviour
{
    public Vector3 MoveDirection { get; private set; }

    private Transform target;
    private float decisionInterval = 0.2f;
    private float lastDecisionTime = 0f;

    void Update()
    {
        if (Time.time - lastDecisionTime >= decisionInterval)
        {
            UpdateMoveDirection();
            lastDecisionTime = Time.time;
        }
    }

    void UpdateMoveDirection()
    {
        Transform player = GetClosestPlayer();
        if (player == null)
        {
            MoveDirection = Vector3.zero;
            return;
        }

        Vector3 direction = (player.position - transform.position).normalized;

        // Snap to 8 directions (N, NE, E, SE, S, SW, W, NW)
        float angle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
        float snappedAngle = Mathf.Round(angle / 45f) * 45f;
        float rad = snappedAngle * Mathf.Deg2Rad;
        Vector3 snappedDirection = new Vector3(Mathf.Cos(rad), 0, Mathf.Sin(rad)).normalized;

        MoveDirection = snappedDirection;
        
    }

    Transform GetClosestPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length == 0) return null;

        Transform closest = players[0].transform;
        float minDist = Vector3.Distance(transform.position, closest.position);

        foreach (GameObject p in players)
        {
            float dist = Vector3.Distance(transform.position, p.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                closest = p.transform;
            }
        }

        return closest;
    }
}
