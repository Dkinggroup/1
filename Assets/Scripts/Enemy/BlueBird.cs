using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBird : EnemyControl 
{
    [SerializeField] private Transform checkGround;
    [SerializeField] private LayerMask Ground;

   private bool result;

    private void Update()
    {
        rib.velocity = new Vector2(-speed * transform.localScale.x, rib.velocity.y);
        result = Physics2D.OverlapCircle(checkGround.position, 0.5f, Ground);
        if (!result)
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
}
