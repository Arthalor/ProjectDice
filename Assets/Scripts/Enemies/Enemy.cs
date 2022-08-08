using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Helper.VectorFunctions;
using Helper;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int health = default;
    [SerializeField] private int movementSpeed = default;
    [SerializeField] protected Transform player = default;
    [SerializeField] protected Rigidbody2D rb = default;
    [SerializeField] private float bounceForce = default;

    [Header("Loot")]
    [SerializeField] private GameObject dropItem = default;
    [SerializeField] private float dropChance = default;

    private Vector2 currentVelocity;

    private int currentHealth;

    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentHealth = health;
    }

    protected void AccelerateTowardsPlayer()
    {
        Vector2 targetVelocity = PlayerDirection() * movementSpeed;
        rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref currentVelocity, 0.5f);
    }

    protected Vector2 PlayerDirection()
    {
        return DirectionAtoB(transform.position, player.position);
    }

    protected void KnockBack(Vector2 direction) 
    {
        rb.velocity = Vector2.zero;
        rb.AddForce(direction * bounceForce);
    }

    protected void DealDamage(PlayerStats stats) 
    {
        stats.TakeDamage();
    }

    public void TakeDamage(int damage) 
    {
        currentHealth -= damage;
        if (currentHealth <= 0) Die();
    }

    public void Die() 
    {
        DropItem();
        Destroy(gameObject);
    }

    protected void DropItem() 
    {
        if (dropChance > Random.value)
            Instantiate(dropItem, transform.position, Quaternion.identity);
    }
}