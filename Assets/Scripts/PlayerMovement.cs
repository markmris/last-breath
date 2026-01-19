using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem; 

public class PlayerMovement : MonoBehaviour
{
    public InGameUIControl inGameUIControl;

    public Transform groundCheck;
    public Vector2 groundCheckSize;
    public LayerMask groundLayer;
    public Rigidbody2D rigidBody;
    public Animator animator;
    public float walkSpeed;
    public float jumpHeight;
    public InputActionReference moveAction;
    public InputActionReference jumpAction;
    public InputActionReference attackAction;
    private Vector2 moveDirection;
    
    private bool attacking = false;
    private bool grounded = false;

    public float regenTime;
    private int stamina = 5;
    private double standStillTime = 0f;

    public void Update()
    {
        moveDirection = moveAction.action.ReadValue<Vector2>();
        grounded = Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0f, groundLayer);
        
        if (animator.GetBool("Grounded") != grounded)
        {
            animator.SetBool("Grounded", grounded);
        }

        if (Time.timeAsDouble - standStillTime > regenTime && stamina < 5)
        {
            inGameUIControl.AddStamina(stamina);
            stamina++;
            standStillTime = Time.timeAsDouble;
        }

        if (moveDirection.x != 0 && !attacking && grounded)
        {
            animator.SetBool("Running", true);
            standStillTime = Time.timeAsDouble;
        }
        else
        {
            animator. SetBool("Running", false);
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

    private void Jump(InputAction.CallbackContext obj)
    {
        if (grounded)
        {
            rigidBody.linearVelocity = Vector2.up * jumpHeight;
        }
    }

    private void Attack(InputAction.CallbackContext obj)
    {
        if (!grounded | stamina == 0 | attacking) {return;}

        Debug.Log("ATTACK INPUT");
        attacking = true;
        rigidBody.linearVelocityX = 0;
        animator.SetTrigger("Attack");
        stamina--;
        inGameUIControl.ReduceStamina(stamina);
        standStillTime = Time.timeAsDouble;
    }

    public void OnAttackFinished()
    {
        Thread.Sleep(100);
        attacking = false;
        Debug.Log("FINISHED ATTACK");
    }
}