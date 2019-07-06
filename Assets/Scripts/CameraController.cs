using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float lookAtOffset = 1.2f;
    public float rotationSpeed = 3f;
    public GameObject player;

    private Vector3 offset;
    private int rotation;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    private void Update()
    {
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
        offset = Quaternion.AngleAxis(rotation * rotationSpeed, Vector3.up) * offset;

        transform.position = player.transform.position + offset;

        Vector3 playerPos = player.transform.position;
        // Look slightly above the ball
        playerPos.y += lookAtOffset;

        transform.LookAt(playerPos);
    }
}
