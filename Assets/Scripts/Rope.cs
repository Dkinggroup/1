using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    [SerializeField] private Transform Point;
    [SerializeField] private float speed;

    private Vector3 firstPosition;
    bool active = false;

    private void Start()
    {
        firstPosition = transform.position;
    }

    private void Update()
    {
        if (active)
        {
            transform.position = Vector3.MoveTowards(transform.position, Point.position, speed * Time.deltaTime);
            Debug.Log("1");
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, firstPosition, speed * Time.deltaTime);
            Debug.Log("2");
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(wait(2f));
        }
        else if (collision.gameObject.tag == "Ground"|| collision.gameObject.tag == "Water")
        {
            Debug.Log("3");
            StartCoroutine(Restart(3f));
        }
    }

    private IEnumerator wait(float time)
    {
        yield return new WaitForSeconds(time);
        active = true;
    }

    private IEnumerator Restart(float time)
    {
        yield return new WaitForSeconds(time);
        active = false;
    }
}
