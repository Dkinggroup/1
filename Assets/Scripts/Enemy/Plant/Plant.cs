using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : EnemyControl
{
    [SerializeField] private float coolDownTime;
    [SerializeField] private GameObject[] Bullet;
    [SerializeField] private Transform Point;
    private float waitTime;

    protected override void Awake()
    {
        base.Awake();
    }
    private void Update()
    {
        if (waitTime > coolDownTime)
        {
            Attack();
        }

        waitTime += Time.deltaTime;
    }

    private void Attack()
    {
        anim.SetTrigger("attack");
        waitTime = 0;
        Bullet[Find()].transform.position = Point.transform.position;
        Bullet[Find()].GetComponent<BulletPlant>().SetDirection();
    }

    private int Find()
    {
        for(int i = 0; i < Bullet.Length; i++)
        {
            if (!Bullet[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}
