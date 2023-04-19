using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    public GameObject menuPrincipal;
    public GameObject menuGameOver;
    
    public float runSpeed = 1;
    public float jumpSpeed = 2;
    Rigidbody2D rb2D;
    public bool betterJump = false;
    public float fallMultiplier = 0.5f;
    public float lowJumpMultiplier = 1f;
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    public bool start=false;
    public bool gameOver=false;
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        
    }


    void FixedUpdate()
    {
        if (start == false)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                start = true;
            }
        }

        if (gameOver == true)
        {
            menuGameOver.SetActive(true);
            if (Input.GetKeyDown(KeyCode.X))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        if (start == true)
        {
            menuPrincipal.SetActive(false);
            if (Input.GetKey("d") || Input.GetKey("right"))
            {
                rb2D.velocity = new Vector2(runSpeed, rb2D.velocity.y);
                spriteRenderer.flipX = false;
                animator.SetBool("Run", true);
            }
            else if (Input.GetKey("a") || Input.GetKey("left"))
            {
                rb2D.velocity = new Vector2(-runSpeed, rb2D.velocity.y);
                spriteRenderer.flipX = true;
                animator.SetBool("Run", true);
            }
            else
            {
                rb2D.velocity = new Vector2(0, rb2D.velocity.y);
                animator.SetBool("Run", false);
            }
            if (Input.GetKey("space") && CheckGround.isGrounded)
            {
                rb2D.velocity = new Vector2(rb2D.velocity.x, jumpSpeed);
            }
            if (CheckGround.isGrounded == false)
            {
                animator.SetBool("Jump", true);
                animator.SetBool("Run", false);
            }
            if (CheckGround.isGrounded == true)
            {
                animator.SetBool("Jump", false);

            }
            if (betterJump)
            {
                if (rb2D.velocity.y < 0)
                {
                    rb2D.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier) * Time.deltaTime;
                }

                if (rb2D.velocity.y > 0 && !Input.GetKey("space"))
                {
                    rb2D.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier) * Time.deltaTime;
                }
            }
        }
        
    }
}
