using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playermovement : MonoBehaviour
{
    // Start is called before the first frame update
    
    public CharacterController2D controller;

    float horizontalMove = 0F;
    public float runSpeed = 40F;
    bool jump = false;
    public Vector2 boxSize;
    public float castdistance;
    public LayerMask groundLayer;
    public int enemyHealth;
    public GameObject deathEffect;
    public int pointsOnDeath;
    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 16f;
    private bool isFacingRight = true;

    private bool isJumping;

    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    private float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
   


    void Start()
    {
       
        controller = GetComponent<CharacterController2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        horizontal = Input.GetAxisRaw("Horizontal");

        if (isGrounded())
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f && !isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);

            jumpBufferCounter = 0f;

            StartCoroutine(JumpCooldown());
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);

            coyoteTimeCounter = 0f;
        }

        Flip();


        if (Input.GetButtonDown("Jump")&& isGrounded())
        { 
        jump = true;
        
        }
    }

   

    public bool isGrounded() 
    {
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castdistance, groundLayer))
        {
            return true;
        }
        else 
        { 
            return false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position-transform.up*castdistance, boxSize);
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false , jump);
        jump = false;
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            Vector3 localScale = transform.localScale;
            isFacingRight = !isFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private IEnumerator JumpCooldown()
    {
        isJumping = true;
        yield return new WaitForSeconds(0.4f);
        isJumping = false;
    }
}
