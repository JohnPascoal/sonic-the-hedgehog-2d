using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;
    public Transform groundCheck;
    private Animator anim;

    [SerializeField] private float maxSpeed = 25;
    [SerializeField] private float moveForce = 30;
    [SerializeField] private float jumpForce = 700;
    private float direction;
    private float hForce = 1;
    private bool isGrounded = false;
    private bool isJump = false;
    private bool isSpinDash = false;

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        isGrounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        direction = Input.GetAxis("Horizontal");

        anim.SetBool("OnGround", isGrounded);

        if (isGrounded)
        {
            anim.SetBool("Jump", false);
        }


        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                isJump = true;
                isSpinDash = true;
            }
        }

        /*if(isSpinDash){
            hForce-=0.01f;
            rigidbody2d.linearVelocityX
        }*/
    }

    private void FixedUpdate()
    {
        Running();

        if (isJump)
        {
            Jump();
        }

    }

    private void Running()
    {
        anim.SetFloat("Speed", Math.Abs(rigidbody2d.linearVelocityX));

        rigidbody2d.AddForce(new Vector2(direction * hForce * moveForce, 0));

        if (direction < 0f)
            gameObject.GetComponent<SpriteRenderer>().flipX = false;

        if (direction > 0f)
            gameObject.GetComponent<SpriteRenderer>().flipX = true;

        if (rigidbody2d.linearVelocityX > maxSpeed)
        {
            rigidbody2d.linearVelocity = new Vector2(Math.Sign(rigidbody2d.linearVelocityX) * maxSpeed, rigidbody2d.linearVelocityY);
        }
    }

    private void Jump()
    {
        anim.SetBool("Jump", true);
        rigidbody2d.AddForce(new Vector2(0f, jumpForce));
        isJump = false;
    }

}
