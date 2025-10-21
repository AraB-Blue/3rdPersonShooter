using UnityEngine;
using UnityEngine.InputSystem;

public class Locomotion : MonoBehaviour
{
    float jumpForce;
    float movementForce;

    Vector3 input;
    bool jumpPressed;

    Rigidbody rb;
    PlayerInput playerInput;
    void Start()
    {
        jumpForce = 250f;
        movementForce = 10f;

        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();

        playerInput.actions["Jump"].performed += ctx => jumpPressed = true;

    }

    
    void Update()
    {
        input = playerInput.actions["Move"].ReadValue<Vector2>();

    }

    private void FixedUpdate()
    {
        rb.AddForce(new Vector3(input.x, 0f, input.y) * movementForce);

        if (jumpPressed)
        {
            rb.AddForce(Vector3.up * jumpForce);
            jumpPressed = false;
        }
    }
}
