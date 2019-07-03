using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float inAirSpeed;
    public float jumpSpeed;

    private bool hasStarted = false;

    private AudioSource bumpSound;
    private AudioSource jumpSound;
    private bool inAir;

    // Start is called before the first frame update
    void Start()
    {
        AudioSource[] audioSources = GetComponents<AudioSource>();
        bumpSound = audioSources[0];
        jumpSound = audioSources[1];
    }

    // Update is called once per frame
    void Update()
    {

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
        Vector3 jump = new Vector3(0, 0, 0);

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
                // game over
            } else {
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
