using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Rino : EnemyControl
{ 
    [SerializeField] private Transform GroundCheck;
    [SerializeField] private Transform Left;
    [SerializeField] private Transform Right;
    [SerializeField] private Transform Point;
    [SerializeField] private LayerMask Player;
    [SerializeField] private LayerMask Wall;
    [SerializeField] private GameObject player;

    private bool tmp1, tmp2;

    private Vector3 localScale;

    private void Start()
    {
        localScale = transform.localScale;
    }


    private void Update()
    {
        if (anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "HitWall")
        {
            rib.velocity = new Vector2(2 * transform.localScale.x, rib.velocity.y);
            return;
        }
        else
        {
            rib.velocity = Vector2.zero;
            if (checkWall())
            {
                tmp1 = false;
                StartCoroutine(wait(0.5f));
            }
            else if (checkPlayer() && !tmp2)
            {
                tmp1 = true;
                tmp2 = true;
            }
            if (tmp1)
            { 
                anim.SetBool("run", true);
                if (transform.localScale.x > 0)
                    transform.position = Vector3.MoveTowards(transform.position, Left.position, speed * Time.deltaTime);
                else if (transform.localScale.x < 0)
                    transform.position = Vector3.MoveTowards(transform.position, Right.position, speed * Time.deltaTime);
            }

            if (transform.position == Point.position)
            {
                anim.SetBool("run", false);
                transform.localScale = localScale;
                tmp2 = false;
            }
            if(!tmp1 && tmp2)
                transform.position = Vector3.MoveTowards(transform.position, Point.position, speed * Time.deltaTime);
        }
    }

    private bool checkPlayer()
    {
        bool check = false;
        check = Physics2D.OverlapArea(Left.position, Right.position, Player);
        return check;
    }

    private bool checkWall()
    {
        bool check = false;
        check = Physics2D.OverlapCircle(GroundCheck.position, 0.5f, Wall);
        return check;
    }

    private IEnumerator wait(float time)
    {
        anim.SetTrigger("hitwall");
        yield return new WaitForSeconds(time);
        transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
    }

} 