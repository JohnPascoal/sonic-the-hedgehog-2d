using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;// { get; private set; }

    private Rigidbody2D rgb2d;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask layer;
    private Animator anim;

    [SerializeField] private float maxSpeed = 25;
    [SerializeField] private float moveForce = 30;
    [SerializeField] private float jumpForce = 700;
    private float xAxis;
    private float hForce = 1;
    public bool isGrounded;
    public bool isJump = false;
    private bool isSpinDash = false;
    private bool isHit;

    void Start()
    {
        Instance = this;
        rgb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        //isGrounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.25f, layer);
        //isGrounded=Physics2D.Raycast(transform.position, Vector2.down, 1f, 1 << LayerMask.NameToLayer("Ground"));
        xAxis = Input.GetAxis("Horizontal");

        anim.SetBool("OnGround", isGrounded);

        if (isGrounded)
        {
            anim.SetBool("Jump", false);
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            anim.SetBool("Jump", true);
            //if (isGrounded)
            //{
            isJump = true;
            //isSpinDash = true;
            //}
        }

        isSpinDash = Input.GetKey(KeyCode.DownArrow);
        if (isSpinDash && !isHit)
            anim.SetBool("SpinDash", true);
        else
            anim.SetBool("SpinDash", false);

    }

    private void FixedUpdate()
    {
        Running();

        if (isJump && !isHit)
        {
            Jump();
        }
    }

    private void Running()
    {
        if (Input.GetKey(KeyCode.RightArrow) && rgb2d.linearVelocityX < 10)
        {
            hForce += 0.1f;
        }
        if (!Input.GetKey(KeyCode.RightArrow))
        {
            hForce = 1;
        }

        anim.SetFloat("Speed", Math.Abs(rgb2d.linearVelocityX));

        rgb2d.AddForce(new Vector2(xAxis * hForce * moveForce, 0));

        if (xAxis < 0f)
            gameObject.GetComponent<SpriteRenderer>().flipX = false;

        if (xAxis > 0f)
            gameObject.GetComponent<SpriteRenderer>().flipX = true;

        if (Math.Abs(rgb2d.linearVelocityX) > maxSpeed)
        {
            rgb2d.linearVelocity = new Vector2(Math.Sign(rgb2d.linearVelocityX) * maxSpeed, rgb2d.linearVelocityY);
        }

    }

    public void EndHit()
    {
        isHit = false;
    }

    public void GetHit()
    {
        if (isHit)
        {
            return;
        }
        else
        {
            isHit = true;
            anim.SetTrigger("Hit");
            rgb2d.linearVelocity = Vector2.zero;
            var direction = gameObject.GetComponent<SpriteRenderer>().flipX ? -1 : 1;
            rgb2d.AddForce(new Vector2(10 * direction, 10), ForceMode2D.Impulse);
        }
    }

    private void Jump()
    {
        //anim.SetBool("Jump", true);
        rgb2d.AddForce(new Vector2(0f, jumpForce));
        isJump = false;
    }

    public void AddForceOnImpact(float force)
    {
        rgb2d.linearVelocity = Vector2.zero;
        rgb2d.AddForce(Vector2.up * force, ForceMode2D.Impulse);
    }

}
