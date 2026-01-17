using UnityEngine;
using UnityEngine.InputSystem; 

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private SpriteRenderer animator;
    private float walkSpeed = 10f;
    private float jumpHeight = 12f;

    private InputActionAsset InputActions;

    private InputAction moveAction;
    private InputAction jumpAction;

    private Vector2 moveInput;
    private Vector2 jumpInput;

    private void OnEnable()
    {
        InputActions.FindActionMap("Player").Enable();
    }

    private void OnDisable()
    {
        InputActions.FindActionMap("Player").Disable();
    }

    private void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");

        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        moveInput = moveAction.ReadValue<Vector2>();

        if (jumpAction.WasPressedThisFrame())
        {
            Jump();
        }

        //rigidBody.MovePosition(transform.forward * moveInput.y * walkSpeed * Time.deltaTime + rigidBody.position);
    }

    private void Jump()
    {
        rigidBody.AddForceAtPosition(new Vector2(0, jumpHeight), Vector2.up, ForceMode2D.Impulse);
    }
}
