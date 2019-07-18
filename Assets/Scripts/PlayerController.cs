using UnityEngine;
using UnityEngine.Events;

// This is necessary to expose the event in the editor inspector.
[System.Serializable]
public class UnityEventWithIntParam : UnityEvent<int> { }

/*
 * Player controller. Listens for control input and applies force to body.
 * Movement direction is relative to camera direction.
 * Plays sounds on collisions and jumps.
 */
public class PlayerController : MonoBehaviour
{
    public int initialJumps;
    public float speed;
    public float inAirSpeed;
    public float jumpSpeed;
    public GameObject followCamera;
    public AudioSource bumpSound;
    public AudioSource jumpSound;

    public UnityEventWithIntParam onJumpUpdate;
    public UnityEvent onInAirCollision;
    public UnityEvent onCollision;

    private bool hasStarted;

    private bool jumpPressed;
    private bool inAir;

    private int _jumps;

    public int Jumps {
        get {
            return _jumps;
        }
        set {
            _jumps = value;
            onJumpUpdate.Invoke(value);
        }
    }

    void Start()
    {
        Jumps = initialJumps;
    }

    private void Update()
    {
        jumpPressed = Input.GetKeyDown(KeyCode.Space);
    }

    void FixedUpdate()
    {
        if (!hasStarted) {
            return;
        }

        float moveH = Input.GetAxis("Horizontal");
        float moveV = Input.GetAxis("Vertical");

        float factor = speed;

        Rigidbody body = GetComponent<Rigidbody>();
        Vector3 movement = new Vector3(moveH, 0, moveV);

        movement = followCamera.transform.TransformDirection(movement);
        movement.y = 0;

        if (inAir) {
            factor = inAirSpeed;
        } else if (jumpPressed && Jumps > 0) {
            inAir = true;
            Jumps--;
            jumpSound.Play();
            body.AddForce(new Vector3(0, 10, 0) * jumpSpeed * Time.deltaTime);
        }

        body.AddForce(movement * factor * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Ground") {
            hasStarted = true;
            inAir = false;
        }

        if (collision.gameObject.tag == "Wall") {
            if (inAir) {
                onInAirCollision.Invoke();
            } else {
                onCollision.Invoke();
                bumpSound.Play();
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "Ground") {
            inAir = true;
        }
    }
}
