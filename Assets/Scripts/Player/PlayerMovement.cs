using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Player player;
    private Vector2 moveDirections = Vector2.zero;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        UpdateDirections();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector2 moveVector = moveDirections * player.moveSpeed * Time.deltaTime;
        transform.position += new Vector3(moveVector.x, moveVector.y, 0);
    }

    private void UpdateDirections()
    {
        moveDirections.x = Input.GetAxisRaw("Horizontal");
        moveDirections.y = Input.GetAxisRaw("Vertical");
    }
}
