using Unity.VisualScripting.Antlr3.Runtime;
using UnityEditor.Callbacks;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ZoombieController : MonoBehaviour
{
    public float moveSpeed = 2f;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Transform closestTarget = GetClosestPlayer();
        if (closestTarget == null) return;

        Vector2 direction = ((Vector2)closestTarget.position - rb.position).normalized;
        Debug.Log(direction);
        Vector2 newPos = rb.position + direction * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(newPos);
    }

    Transform GetClosestPlayer()
    {

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length == 0) return null;

        Transform player = players[0].transform;

        float minDist = Vector2.Distance(transform.position, player.position);

        foreach (GameObject p in players)
        {
            float dist = Vector2.Distance(transform.position,
                p.transform.position);
            if (minDist > dist)
            {
                minDist = dist; ;
                player = p.transform;
            }
        }

        return player;
    }
}
