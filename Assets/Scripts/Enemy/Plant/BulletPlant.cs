using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlant : MonoBehaviour
{
    [SerializeField] private float speed;

    private float timereset;
    private bool hit;
    private CircleCollider2D circleCollider;

    private void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        if (hit) return;

        float movementspeed = -speed *Time.deltaTime;

        if(timereset > 2f)
        {
            gameObject.SetActive(false);
            timereset = 0; 
        }
        timereset += Time.deltaTime;
        transform.Translate(movementspeed, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        circleCollider.enabled = false;
        gameObject.SetActive(false);
    }

    public void SetDirection()
    {
        gameObject.SetActive(true);
        circleCollider.enabled = true;
        hit = false;
        Debug.Log("1");
    }
}
