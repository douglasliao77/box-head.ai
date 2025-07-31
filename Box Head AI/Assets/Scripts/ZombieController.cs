using UnityEngine;

public class ZoombieController : MonoBehaviour
{
    public float moveSpeed = 2f;
    public LayerMask wallLayerMask;
    private readonly float rotationSpeed = 2f;
    public float knockbackResistance = 1f;
    private Rigidbody rb;
    private ZombieAI ai;
    private Vector3 currentDirection;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        ai = GetComponent<ZombieAI>();
        currentDirection = transform.forward;
    }

    void FixedUpdate()
    {
        Vector3 moveDir = ai.MoveDirection;

        if (moveDir != Vector3.zero)
        {
            Vector3 newPos = rb.position + moveDir * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(newPos);

            Quaternion toRotation = Quaternion.LookRotation(moveDir, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed * Time.fixedDeltaTime);
        }

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        animator.SetFloat("Speed", moveDir.magnitude);
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
