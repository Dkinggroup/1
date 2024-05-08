using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletTruck : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform Trunk;

    private CircleCollider2D circleCollider;
    private Animator anim;

    private bool hit;
    private float direction;
    private float LifeTime;

    private void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (hit) return;
        float movementspeed = -speed * Time.deltaTime * direction;
        transform.Translate(movementspeed, 0, 0);

        if(LifeTime > 3)
        {
            gameObject.SetActive(false);
        }
        LifeTime += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        circleCollider.enabled = false;
        anim.SetTrigger("piece");
    }

    public void SetDirection(float _direction)
    {
        LifeTime = 0;
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        circleCollider.enabled = true;

        float scaleX = transform.localScale.x;

        if (Mathf.Sign(scaleX) != _direction)
            scaleX = -scaleX;

        transform.localScale = new Vector3(scaleX,transform.localScale.y,transform.localScale.z);   

    }

    private void Destroy()
    {
        gameObject.SetActive(false);
    }
}
