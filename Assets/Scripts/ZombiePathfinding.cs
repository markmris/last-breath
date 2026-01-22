using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class ZombiePathfinding : MonoBehaviour
{
    public GameObject player;
    private Rigidbody2D rigidBody;
    public float walkSpeed;
    public float reach = 0.2f;
    private int direction;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
    }

    void FixedUpdate()
    {
        direction = Math.Sign(player.transform.position.x - transform.position.x);

        rigidBody.linearVelocity = new Vector2(direction * walkSpeed, rigidBody.linearVelocityY);
    }
}
