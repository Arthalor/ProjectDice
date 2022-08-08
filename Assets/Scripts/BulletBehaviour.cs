using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helper;
using static Helper.VectorFunctions;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb = default;
    [SerializeField] private SpriteRenderer sRenderer = default;

    [SerializeField] private Sprite[] sprites;

    private Vector2 startPosition;
    private bool shot = false;

    private int damage;
    private int speed;
    private int range;

    private bool collided = false;
    [SerializeField] private float shrinkAmountFrame = default;

    private void Update()
    {
        if (collided) 
        {
            ShrinkingBehaviour();
        }
        else
            DestroyOnEndOfRange();
    }

    public void SetStats(int d, int s, int r) 
    {
        damage = Mathf.Max(d,1);
        speed = Mathf.Max(s, 1);
        range = Mathf.Max(r, 1);

        SetSprite();
    }

    private void SetSprite() 
    {
        sRenderer.sprite = sprites[damage - 1];
    }

    public void Shoot() 
    {
        transform.parent = null;
        rb.velocity = transform.up * speed;
        startPosition = transform.position;
        shot = true;
    }

    public void DestroyOnEndOfRange() 
    {
        if (!shot) return;
        if (!SqrDistanceSmaller(startPosition, Vector2NoZ(transform.position), range)) Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(damage);
            collision.otherCollider.enabled = false;
            collided = true;
        }
    }

    private void ShrinkingBehaviour() 
    {
        if (transform.localScale.magnitude > 0.05f)
            transform.localScale = transform.localScale - Vector3.one * shrinkAmountFrame;
        else Destroy(gameObject);
    }
}