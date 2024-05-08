using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : Trap
{
    [SerializeField] private float speed;
    [SerializeField] private Transform Right;
    [SerializeField] private Transform left;

    private void Update()
    {
        rib.velocity = new Vector2(speed * transform.localScale.x , rib.velocity.y);

        if (transform.position.x > Right.position.x)
            transform.localScale = new Vector3(-1, 1, 1);
        else if (transform.position.x < left.position.x)
            transform.localScale = new Vector3(1, 1, 1);
    }

}
