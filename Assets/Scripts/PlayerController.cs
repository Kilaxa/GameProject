using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private bool onGround;
    private Vector2 moveVector;

    public Camera cam;

    public float speed = 15f;
    public float jumpForce = 3500f;
    public float fallingForce = 5f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        ///// Movement /////

        // WASD
        moveVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        rb.AddForce(new Vector3(moveVector.x, 0, moveVector.y) * speed);
        transform.rotation = new Quaternion(transform.rotation.x, cam.transform.rotation.y, 0, 1);


        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            rb.AddForce(Vector3.up * jumpForce);
        }
        if (rb.velocity.y < 0)
        {
            rb.AddForce(Vector3.down * fallingForce);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            onGround = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            onGround = false;
        }
    }
}
