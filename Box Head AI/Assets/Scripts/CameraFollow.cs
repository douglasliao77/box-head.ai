using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform player;           // Player to follow
    public Vector3 offset = new Vector3(0, 15, -10);  // Camera offset
    public float smoothSpeed = 0.125f;  // How smooth the camera follows

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;
        else
            Debug.LogWarning("No GameObject with tag 'Player' found!");
    }

    void LateUpdate()
    {
        if (player == null) return;

        Vector3 desiredPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = desiredPosition;

        transform.LookAt(player);
    }
}
