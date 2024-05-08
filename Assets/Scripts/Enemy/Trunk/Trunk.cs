using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trunk : EnemyControl
{
    [SerializeField] private Transform Left;
    [SerializeField] private Transform Right;
    [SerializeField] private Transform Point;
    [SerializeField] private GameObject[] Bullet;
    [SerializeField] private float AttackRange;
    [SerializeField] private float AttackCoolDownTime;


    private float CoolDownTime = Mathf.Infinity;

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
        if (Player.transform.position.x >= Left.position.x && Right.position.x >= Player.transform.position.x)
        {
            if (Player.GetComponent<PlayerControl>().isground())
            {
                float distance = Vector2.Distance(transform.position, Player.transform.position);
                transform.localScale = new Vector3(-Mathf.Sign(Player.transform.position.x - transform.position.x), 1, 1);
                if (distance <= AttackRange)
                {
                    if(CoolDownTime > AttackCoolDownTime)
                        Attack();
                    else
                        anim.SetBool("run", false);
                }
                else
                {
                    anim.SetBool("run", true);
                    transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);
                }
            }
            else
                anim.SetBool("run", false);
        }
        else
            anim.SetBool("run", false);

        CoolDownTime += Time.deltaTime;


    }

    private void Attack()
    {
        CoolDownTime = 0;
        anim.SetTrigger("attack");

        Bullet[Find()].transform.position = Point.position;
        Bullet[Find()].GetComponent<BulletTruck>().SetDirection(transform.localScale.x);
    }

    private int Find()
    {
        for(int i = 0 ; i < Bullet.Length; i++)
        {
            if (!Bullet[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}
