using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RockHead : MonoBehaviour
{
    [SerializeField] private Transform Point;
    [SerializeField] private float speed;
    [SerializeField] private float cooldowntime;

    private bool move;
    private bool active;
    private Animator anim;
    private Vector3 firstposition;

    private void Awake()
    {
        firstposition = transform.position;
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (move)
        {
            transform.position = Vector3.Lerp(transform.position, Point.position,speed *Time.deltaTime);
            if (!active)
            {
                anim.SetTrigger("active");
                active = true;
            }
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, firstposition , speed *Time.deltaTime);
            if (!active)
            {
                anim.SetTrigger("active");
                active = true;
            }
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            ReloadScene();
        }
        else if(collision.gameObject.tag == "Wall")
        {
            active = false;
            StartCoroutine(wait(cooldowntime));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Point")
        {
            active = false;
            StartCoroutine(waitreturn(cooldowntime));
        }
    }

    private IEnumerator wait(float time)
    {
        yield return new WaitForSeconds(time);
        move = true;
    }

    private IEnumerator waitreturn(float time)
    {
        yield return new WaitForSeconds(time);
        move = false;
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
