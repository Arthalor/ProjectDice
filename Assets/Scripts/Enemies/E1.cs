using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Helper.VectorFunctions;
using Helper;

public class E1 : Enemy
{ 
    private void Update()
    {
        AccelerateTowardsPlayer();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        KnockBack(DirectionAtoB(collision.transform.position, transform.position));

        if (collision.collider.TryGetComponent(out PlayerStats player)) 
        {
            DealDamage(player);
        }
    }
}