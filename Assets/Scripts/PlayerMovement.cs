using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem; 

public class PlayerMovement : MonoBehaviour
{
    public InGameUIControl inGameUIControl;
    public GameOver gameOver;
    public GameObject attackOrb;
    private SpriteRenderer newOrb;
    public Rigidbody2D rigidBody;
    public Animator animator;
    public float walkSpeed;
    public float jumpHeight;
    public InputActionReference moveAction;
    public InputActionReference jumpAction;
    public InputActionReference attackAction;
    private Vector2 moveDirection;

    public SoundManager soundManager;
    public AudioClip attack;

    public bool attacking = false;
    public bool grounded = false;

    public float regenTime;
    private int stamina = 5;
    private double standStillTime = 0f;
    public float health = 100f;

    public void Update()
    {
        if (health <= 0)
        {
            gameOver.ResetGame();
            Destroy(this);
        }

        moveDirection = moveAction.action.ReadValue<Vector2>();

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
        if (collision.gameObject.CompareTag("Ground"))
        {
        grounded = true;
        animator.SetBool("Grounded", true);
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
        grounded = false;
        animator.SetBool("Grounded", false);
        }
    }

    private void Jump(InputAction.CallbackContext obj)
    {
        if (grounded && !attacking)
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

        newOrb = Instantiate(attackOrb, transform).GetComponent<SpriteRenderer>();
        newOrb.sortingLayerName = "VFX";
        Color newColor = newOrb.color;
        newColor.a = .45f;
        newOrb.color = newColor;
        newOrb.transform.position = new Vector2(newOrb.transform.position.x, newOrb.transform.position.y - .75f);
        soundManager.PlaySFX(attack);
    }

    public void OnAttackFinished()
    {
        attacking = false;
        Debug.Log("FINISHED ATTACK");
        Destroy(newOrb.gameObject);
    }
}