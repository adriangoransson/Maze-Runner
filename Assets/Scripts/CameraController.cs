using UnityEngine;

/*
 * Main camera controller to stay behind player and listen for rotations.
 */
public class CameraController : MonoBehaviour
{
    public float lookAtOffset = 1.2f;
    public float rotationSpeed = 3f;
    public GameObject player;

    private Vector3 offset;
    private int rotation;

    void Start()
    {
        // Set initial offset as the distance in the editor.
        offset = transform.position - player.transform.position;
    }

    private void Update()
    {
        // Listen for rotation events, Q/E rotates left/right.
        if (Input.GetKey(KeyCode.Q)) {
            rotation = -1;
        } else if (Input.GetKey(KeyCode.E)) {
            rotation = 1;
        } else {
            rotation = 0;
        }
    }

    void LateUpdate()
    {
        // Rotate around the Y axis.
        offset = Quaternion.AngleAxis(rotation * rotationSpeed, Vector3.up) * offset;

        transform.position = player.transform.position + offset;

        Vector3 playerPos = player.transform.position;
        // Look slightly above the ball.
        playerPos.y += lookAtOffset;

        transform.LookAt(playerPos);
    }
}
