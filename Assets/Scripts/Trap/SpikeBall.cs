using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBall : Trap
{
    [SerializeField] private float speed;

    private float tmp;

    protected override void Awake()
    {
        base.Awake();
        tmp = 1;
    }

    private void Update()
    {
        rib.angularVelocity = speed * tmp * transform.localScale.x;
    }
}
