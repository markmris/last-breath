using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ZombiePathfinding : MonoBehaviour
{
    public GameObject player;
    public PlayerMovement playerController;
    public HealthBarManager healthBarManager;
    private Rigidbody2D rigidBody;
    public float walkSpeed;
    public float reach = 0.2f;
    public float debounceTime;
    private int direction;
    private bool debounce = false;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerMovement>();
        healthBarManager = GameObject.Find("HealthContainer").GetComponent<HealthBarManager>();
    }

    void Update()
    {
        float distX = Math.Abs(player.transform.position.x - transform.position.x);
        float distY = Math.Abs(player.transform.position.y - transform.position.y);

        if (distX < reach && distY < reach && !debounce)
        {
            Debug.Log("IN REACH");
            debounce = true;
            playerController.health -= 10f;
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
