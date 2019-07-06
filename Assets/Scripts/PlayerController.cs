using UnityEngine;

/*
 * Player controller. Listens for control input and applies force to body.
 * Movement direction is relative to camera direction.
 * Plays sounds on collisions and jumps.
 */
public class PlayerController : MonoBehaviour
{
    public float speed;
    public float inAirSpeed;
    public float jumpSpeed;
    public GameObject followCamera;

    private bool hasStarted;

    private GameController controller;
    private AudioSource bumpSound;
    private AudioSource jumpSound;
    private bool inAir;

    void Start()
    {
        GameObject go = GameObject.FindGameObjectWithTag("GameController");
        controller = go.GetComponent<GameController>();

        AudioSource[] audioSources = GetComponents<AudioSource>();
        bumpSound = audioSources[0];
        jumpSound = audioSources[1];
    }

    void FixedUpdate()
    {
        if (!hasStarted) {
            return;
        }

        float moveH = Input.GetAxis("Horizontal");
        float moveV = Input.GetAxis("Vertical");
        bool jumpPressed = Input.GetKeyDown(KeyCode.Space);

        float factor = speed;

        Rigidbody body = GetComponent<Rigidbody>();
        Vector3 movement = new Vector3(moveH, 0, moveV);

        movement = followCamera.transform.TransformDirection(movement);
        movement.y = 0;

        Vector3 jump = Vector3.zero;

        if (inAir) {
            factor = inAirSpeed;
        } else if (jumpPressed) {
            jump.y = 10;
            jumpSound.Play();
        }

        body.AddForce(movement * factor * Time.deltaTime);
        body.AddForce(jump * jumpSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Ground") {
            hasStarted = true;
            inAir = false;
        }

        if (collision.gameObject.tag == "Wall") {
            if (inAir) {
                controller.GameOver();
            } else {
                controller.AddPenalty();
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
