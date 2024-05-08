using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    [SerializeField] public int StartHealth;
    [SerializeField] public int Damage;
    [SerializeField] public float speed;

    private float CurrentHealth;

    protected Rigidbody2D rib;
    protected Animator anim;
    protected SpriteRenderer sprite;


    protected virtual void Awake()
    {
        rib = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        CurrentHealth = StartHealth;
    }

    public void JumpedOn() 
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth - 1, 0, StartHealth);

        if(CurrentHealth < 1)
        {
            anim.SetTrigger("hit");
            rib.velocity = Vector2.zero;
            GetComponent<Collider2D>().enabled = false;
            Destroy(this.gameObject);
        }
        else
        {
            anim.SetTrigger("hit");
            StartCoroutine(wait(2));
        }
    }

    private IEnumerator wait(float time)
    {
        rib.velocity = Vector2.zero;
        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(time);
        GetComponent<Collider2D>().enabled = true;
        rib.velocity = new Vector2(speed, rib.velocity.y);
    }
}
