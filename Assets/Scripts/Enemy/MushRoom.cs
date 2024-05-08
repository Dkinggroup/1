using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushRoom : EnemyControl
{
    [SerializeField] private Transform Left;
    [SerializeField] private Transform Right;
    protected override void Awake()
    {
        base.Awake();
    }

    private void Update()
    {
        if(transform.position.x > Left.position.x && transform.position.x < Right.position.x)
        {
            rib.velocity = new Vector2(-speed *transform.localScale.x, rib.velocity.y);
            anim.SetBool("idle", false);
            anim.SetBool("run", true);
        }
        else if(transform.position.x >= Right.position.x)
        {
            transform.localScale = Vector3.one;
            anim.SetBool("idle", true);
            anim.SetBool("run", false);
            StartCoroutine(wait(1));
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
            anim.SetBool("idle", true);
            anim.SetBool("run", false);
            StartCoroutine(wait(1));
        }
    }

    private IEnumerator wait(float time)
    {
        rib.velocity = Vector2.zero;
        yield return new WaitForSeconds(time);
        rib.velocity = new Vector2(-speed * transform.localScale.x, rib.velocity.y);
    }

}
