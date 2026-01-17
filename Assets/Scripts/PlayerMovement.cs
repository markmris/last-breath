using UnityEngine;
using UnityEngine.InputSystem; 

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    public float walkSpeed;
    public float jumpHeight;
    public InputActionReference moveAction;
    public InputActionReference jumpAction;
    private Vector2 moveDirection;
    private bool canJump;

    private void Update()
    {
        moveDirection = moveAction.action.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        rigidBody.linearVelocity = new Vector2(moveDirection.x * walkSpeed, rigidBody.linearVelocityY);
    }

    private void OnEnable()
    {
        
        jumpAction.action.started += Jump;
    }

    private void OnDisable()
    {
        jumpAction.action.started -= Jump;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        canJump = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        canJump = false;
    }

    private void Jump(InputAction.CallbackContext obj)
    {
        if (canJump)
        {
            rigidBody.linearVelocity = Vector2.up * jumpHeight;
        }
    }
}
