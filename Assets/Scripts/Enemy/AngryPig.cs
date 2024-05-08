using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AngryPig : EnemyControl
{
    [SerializeField] private Transform Left;
    [SerializeField] private Transform Right; 
    [SerializeField] private float RunSpeed;

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
        if(Player.transform.position.x >= Left.position.x && Player.transform.position.x <= Right.position.x && Player.GetComponent<PlayerControl>().isground() && Player.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.name != "Hit")
        {
            rib.velocity = Vector2.zero;
            transform.localScale = new Vector3(Mathf.Sign(transform.position.x - Player.transform.position.x), 1, 1);
            transform.position = Vector3.Lerp(transform.position, Player.transform.position, RunSpeed * Time.deltaTime);
            anim.SetBool("run", true);
        }
        else 
            Movement();
    }

    private  void Movement()
    {
        if (transform.position.x > Left.position.x && transform.position.x < Right.position.x)
        {
            rib.velocity = new Vector2(-speed * transform.localScale.x, rib.velocity.y);
        }
        else if (transform.position.x >= Right.position.x)
        {
            transform.localScale = Vector3.one;
            rib.velocity = new Vector2(-speed * transform.localScale.x, rib.velocity.y);

        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
            rib.velocity = new Vector2(-speed * transform.localScale.x, rib.velocity.y);

        }

        anim.SetBool("run", false);
    }
}
