using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb = default;
    [SerializeField] private PlayerInput pInput = default;
    [SerializeField] private PlayerStats stats = default;

    private float moveSpeed;

    private void Update()
    {
        moveSpeed = stats.moveSpeed.value + 1;
        rb.velocity = pInput.movementInput * moveSpeed;
    }
}