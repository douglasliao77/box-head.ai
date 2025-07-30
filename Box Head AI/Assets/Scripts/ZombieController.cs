using UnityEngine;

public class ZoombieController : MonoBehaviour
{
    public float moveSpeed = 2f;
    private readonly float rotationSpeed = 2f;
    public float knockbackResistance = 1f;
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

        if (direction != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed * Time.fixedDeltaTime);
        }
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
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

    public void Knockback(Vector3 direction, float force)
    {
        if (rb)
        {
            Debug.Log("Applying knockback to " + rb.name);
            rb.AddForce(direction * force, ForceMode.Impulse);
        }
        else
        {
            Debug.LogWarning("No Rigidbody found on hit object or its parents!");
        }
    }
}
