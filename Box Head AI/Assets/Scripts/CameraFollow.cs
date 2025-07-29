using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform player;           // Player to follow
    public Vector3 offset = new Vector3(0, 15, -10);  // Camera offset
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
        transform.position = desiredPosition;

        transform.LookAt(player);
    }
}
