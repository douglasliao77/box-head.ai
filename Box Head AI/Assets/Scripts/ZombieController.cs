using UnityEngine;

public class ZoombieController : MonoBehaviour
{
    public float moveSpeed = 2f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Transform closestTarget = GetClosestPlayer();
        if (closestTarget == null)
        {
            Debug.LogWarning("No player found!");
            return;
        }

        Vector3 direction = (closestTarget.position - rb.position).normalized;
        Vector3 newPos = rb.position + direction * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(newPos);
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
