using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private bool onGround;
    
    private Animator animator;

    public Transform aimTarget;
    public float turnSpeed = 1.0f;

    public float speed = 15f;
    public float jumpForce = 3500f;
    public float fallingForce = 5f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        MoveController();
        JumpController();
        RotationController();
        AnimationController();
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
    void MoveController()
    {
        // WASD
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
        {
            rb.AddForce((transform.forward + (transform.right * -1)) * speed / Mathf.Sqrt(2));
        }
        else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
        {
            rb.AddForce((transform.forward + transform.right) * speed / Mathf.Sqrt(2));
        }
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
        {
            rb.AddForce(((transform.forward * -1) + (transform.right * -1)) * speed / Mathf.Sqrt(2));
        }
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
        {
            rb.AddForce(((transform.forward * -1) + transform.right) * speed / Mathf.Sqrt(2));
        }
        else if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(transform.forward * speed);
            animator.SetBool("Walk F", true);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(transform.forward * -1 * speed);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(transform.right * -1 * speed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(transform.right * speed);
        }
    }
    void JumpController()
    {
        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            rb.AddForce(Vector3.up * jumpForce);
        }
        //if (rb.velocity.y < 0)
        //{
        //    rb.AddForce(Vector3.down * fallingForce);
        //}
    }
    void RotationController()
    {
        // Rotate with camera
        if (rb.velocity.x != 0 || rb.velocity.z != 0)
        {
            Vector3 direction = aimTarget.position - rb.position;
            direction.y = 0;
            rb.rotation = Quaternion.Slerp(rb.rotation, Quaternion.LookRotation(direction), turnSpeed * Time.deltaTime);
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            aimTarget.position = ray.GetPoint(15);
        }
    }

    void AnimationController()
    {
        if(Input.anyKey == false)
        {
            animator.SetBool("Walk F", false);
        }
    }
}
