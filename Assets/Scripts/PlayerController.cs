using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float inAirSpeed;
    public float jumpSpeed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        bool inAir = transform.position.y > 0.51;
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
        }

        body.AddForce(movement * factor * Time.deltaTime);
        body.AddForce(jump * jumpSpeed * Time.deltaTime);
    }
}
