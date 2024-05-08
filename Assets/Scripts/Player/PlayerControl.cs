using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private float JumpForce;
    [SerializeField] private float DoubleJumpForce;
    [SerializeField] private float HurtForce;
    [SerializeField] private float AddForce;

    public ParticleSystem dust;

    private Rigidbody2D rib;
    private Animator anim;
    private SpriteRenderer spriteRenderer;

    private enum State {Idle,Run,Jump,DoubleJump,Fall,WallJump,Hit};
    private State state = State.Idle;

    private float horizontalInput;
    private bool  jumpInput;
    private bool DoubleJump;
    private bool CanJump;
    private bool isAttacked;

    private bool Jumping;
    private bool DoubleJumping;

    private bool isGrounded;
    private bool onWall;

    float tmp;

    private void Awake()
    {
        rib = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Movement();

        AnimationState();
    }
    private void Run()
    {
        rib.velocity = new Vector2(Speed * horizontalInput, rib.velocity.y);
        if (horizontalInput > 0)
        {
            transform.localScale = Vector3.one;
        }
        else if(horizontalInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public void Jump()
    {
        if (isGrounded || onWall || CanJump)
        {
            createDust();
            rib.velocity = new Vector2(rib.velocity.x, JumpForce);
            DoubleJump = true;
            isGrounded = false;
            onWall = false;
            Jumping = true;
            CanJump = false;
        }
        else if (DoubleJump)
        {
            rib.velocity = new Vector2(rib.velocity.x, DoubleJumpForce);
            DoubleJump = false;
            isGrounded = false;
            onWall = false;
            Jumping = false;
            DoubleJumping = true;
        }
    }



    private void Movement()
    {
        if (anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Hit")
        {
            rib.velocity = new Vector2(tmp * HurtForce, rib.velocity.y);
            return;
        }
        jumpInput = Input.GetButtonDown("Jump");
        Run();
        if (jumpInput || CanJump)
            Jump();
    }
    
    private void AnimationState()
    {

        if (!isAttacked)
        {
            if (isGrounded)
            {
                if (horizontalInput != 0)
                    state = State.Run;
                else
                    state = State.Idle;
            }
            else
            {
                if (!onWall)
                {
                    if (rib.velocity.y < 0.1f)
                    {
                        state = State.Fall;
                    }
                    else
                    {
                        if (Jumping)
                            state = State.Jump;
                        else if (DoubleJumping)
                        {
                            state = State.DoubleJump;
                        }
                    }
                }
                else
                {
                    if (rib.velocity.y > 0f)
                        state = State.Jump;
                    else
                        state = State.WallJump;
                }
            }
        }
        else
            state = State.Hit;
        anim.SetInteger("state", (int)state);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            createDust();
            isGrounded = true;
            Jumping = false;
            DoubleJump = false;
            DoubleJumping = false;
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            EnemyControl enemy = collision.gameObject.GetComponent<EnemyControl>();

            if (state == State.Fall)
            {
                CanJump = true;
                enemy.JumpedOn();
            }
            else if(!isAttacked)
            {
                gameObject.GetComponent<Health>().TakeDamage(1);
                StartCoroutine(wait());
                if (collision.gameObject.transform.position.x > transform.position.x)
                {
                    this.tmp = -1;
                }
                else
                {
                    this.tmp = 1;
                }
            }
        }
        else if(collision.gameObject.tag == "Trampoline")
        {
            rib.velocity = new Vector2(rib.velocity.x, AddForce);
            DoubleJump = true;
            DoubleJumping = false;
            Jumping = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            onWall = true;
        }
        else if(collision.gameObject.tag == "Rope")
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            onWall = false;
        }
        else if (collision.gameObject.tag == "Ground" ||collision.gameObject.tag == "Rope")
        {
            isGrounded = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Bullet" && !isAttacked)
        {
            StartCoroutine(wait());
            gameObject.GetComponent<Health>().TakeDamage(1);
        }
        else if(collision.tag == "Water")
        {
            ReloadScene();
        }
    }

    


    private IEnumerator wait()
    {
        isAttacked = true;
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds((float)1);
        isAttacked = false;
        spriteRenderer.color = Color.white;
    }

    public bool isground()
    {
        return isGrounded;
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void createDust()
    {
        dust.Play();
    }

    public void HorizontalInput(float value)
    {
        horizontalInput = value;
    }

}
