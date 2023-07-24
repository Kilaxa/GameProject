using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;

    public float moveSpeed = 6f;
    public float turnSmoothTime = 0.1f;
    private Vector3 direction;
    float turnSmoothVelocity;

    private Vector3 velocity;
    public float jumpHeight = 10.0f;
    public float gravityValue = -9.81f;

    void Update()
    {
        MoveController();
        JumpController();
    }

    public void OnMove(InputValue value)
    {
        float horizontal = value.Get<Vector2>().x;
        float vertical = value.Get<Vector2>().y;
        direction = new Vector3 (horizontal, 0f, vertical);
    }

    private void MoveController()
    {
        if (direction.magnitude >= 0.1f)
        {
            float turnAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, turnAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, turnAngle, 0f) * Vector3.forward;

            controller.Move(moveDirection * moveSpeed * Time.deltaTime);
        }
    }

    public void OnJump(InputValue value)
    {
        if(value.isPressed && controller.isGrounded)
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravityValue);
    }

    private void JumpController()
    {
        velocity.y += gravityValue * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
