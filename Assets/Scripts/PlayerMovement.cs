using UnityEngine;
using UnityEngine.InputSystem; 

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    public Animator animator;
    public float walkSpeed;
    public float jumpHeight;
    public InputActionReference moveAction;
    public InputActionReference jumpAction;
    public InputActionReference attackAction;
    private Vector2 moveDirection;

    private bool canJump;
    private bool attacking = false;
    private bool grounded = false;

    public void Update()
    {
        moveDirection = moveAction.action.ReadValue<Vector2>();

        if (moveDirection.x != 0 && !attacking && grounded)
        {
            animator.SetBool("Running", true);
        }
        else
        {
            animator.SetBool("Running", false);
        }
    }

    public void FixedUpdate()
    {
        if (!attacking)
        {
            rigidBody.linearVelocity = new Vector2(moveDirection.x * walkSpeed, rigidBody.linearVelocityY);
        }
    }

    public void OnEnable()
    {
        jumpAction.action.started += Jump;
        attackAction.action.started += Attack;
    }

    public void OnDisable()
    {
        jumpAction.action.started -= Jump;
        attackAction.action.started -= Attack;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        canJump = true;
        grounded = true;
        animator.SetBool("Grounded", true);
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        canJump = false;
        grounded = false;
        animator.SetBool("Grounded", false);
    }

    private void Jump(InputAction.CallbackContext obj)
    {
        if (canJump)
        {
            rigidBody.linearVelocity = Vector2.up * jumpHeight;
        }
    }

    private void Attack(InputAction.CallbackContext obj)
    {
        Debug.Log("ATTACK INPUT");
        attacking = true;
        animator.SetTrigger("Attack");
    }

    public void OnAttackFinished()
    {
        attacking = false;
        Debug.Log("FINISHED ATTACK");
    }
}