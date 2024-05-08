using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : EnemyControl
{
    [SerializeField] private Transform Point;
    [SerializeField] private BoxCollider2D box;
    [SerializeField] private LayerMask player;

    private GameObject Player;
    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (box.IsTouchingLayers(player))
        {
            transform.localScale = new Vector3(Mathf.Sign(transform.position.x - Player.transform.position.x), 1, 1);
            transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, Point.position, speed * Time.deltaTime);
            transform.localScale = new Vector3(Mathf.Sign(transform.position.x - Point.position.x), 1, 1);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Point")
        {
            anim.SetTrigger("out");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Point" && anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Flying")
        {
            anim.SetTrigger("in");
        }
    }
}
