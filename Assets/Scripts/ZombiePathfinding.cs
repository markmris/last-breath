using System;
using System.Collections;
using UnityEngine;

public class ZombiePathfinding : MonoBehaviour
{
    public GameObject player;
    public PlayerMovement playerController;
    public InGameUIControl healthBarManager;
    public ZombieController zombieController;
    private Rigidbody2D rigidBody;
    public float walkSpeed;
    private float playerReach = 1.2f;
    public float zombieReach = 0.2f;
    public float debounceTime;
    private int direction;
    private bool debounce = false;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerMovement>();
        healthBarManager = GameObject.Find("Canvas").GetComponent<InGameUIControl>();
        zombieController = GameObject.Find("ZombieSpawns").GetComponent<ZombieController>();
    }

    void Update()
    {
        float distX = Math.Abs(player.transform.position.x - transform.position.x);
        float distY = Math.Abs(player.transform.position.y - transform.position.y);

        if (distX < playerReach && distY < playerReach && playerController.attacking)
        {
            Destroy(gameObject);
            zombieController.zombieCount--;
        }

        else if (distX < zombieReach && distY < zombieReach && !debounce)
        {
            Debug.Log("IN REACH");
            debounce = true;
            playerController.health -= 7f;
            healthBarManager.ChangeHealth(playerController.health);

            StartCoroutine(ResetDebounce());
        }
    }

    void FixedUpdate()
    {
        direction = Math.Sign(player.transform.position.x - transform.position.x);

        rigidBody.linearVelocity = new Vector2(direction * walkSpeed, rigidBody.linearVelocityY);
    }

    private IEnumerator ResetDebounce()
    {
        yield return new WaitForSeconds(debounceTime);

        debounce = false;

        yield break;
    }
}
