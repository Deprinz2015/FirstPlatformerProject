using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerMovement : MonoBehaviour
{
    
    // ---------------------- VARIABLES -----------------------------------
    [SerializeField] protected float speed = 10;
    [SerializeField] protected float jumpForce = 25;
    [SerializeField] protected Transform groundChecker; 
    [SerializeField] protected LayerMask canJumpFrom;
    [SerializeField] protected float checkGroundRadius = 0.2f; 
    [SerializeField] protected float lowJumpMultiplier = 1.1f;
    [SerializeField] protected float fallMultiplier = 1.1f;
    protected bool isGrounded = false;
    [SerializeField] protected float rememberGroundedFor = 0.1f;
    protected float lastTimeGrounded;
    protected bool facingRight = true;
    protected Rigidbody2D rb;
    protected Animator animator;

    public bool canControl = false;


    // ----------------- OVERRIDE METHODS ------------------------------

    // Abstract Methods -> Override in ChildClass
    protected abstract void Animate();  


    // ----------------- OPTIONAL METHODS ------------------------------

    // Override if jump behaviour is custom
    protected virtual void Jump()
    {
        isGrounded = false;
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }
    
    // Override if extra jumping Checks (e.g. double Jumps) are needed
    protected virtual bool CanJump()
    { return false; }

    // Override if needed
    protected virtual void CustomUpdate() {}
    protected virtual void CustomFixedUpdate() {}

    protected virtual void DoIfGrounded()
    {
        isGrounded = true;
    }
    protected virtual void DoIfNotGrounded()
    {
        if(isGrounded)
        {
            lastTimeGrounded = Time.time;
        }
        isGrounded = false;
    }

    // ----------------- PLAYERMOVEMENT-LOGIC ------------------------------
    // ONLY CHANGE IF FUNDAMENTALLY NEEDED

    // Using Awake because with Start() the Objects dont get initialised...
    // Start() is free to use in Subclass
    void Awake()
    {
        this.rb = this.gameObject.GetComponent<Rigidbody2D>();
        this.animator = this.gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        Animate();
        CustomUpdate();
    }
    
    void FixedUpdate()
    {
        CheckIfGrounded();
        CustomFixedUpdate();
    }

    public void Move(float move, bool jump, bool holdingJump)
    {
        if(!canControl) return;

        rb.velocity = new Vector2(move * speed, rb.velocity.y);

        if(move < 0 && facingRight) { Flip(); }
        else if(move > 0 && !facingRight) { Flip(); }
        
        bool canJump = jump && (isGrounded || Time.time - lastTimeGrounded <= rememberGroundedFor || CanJump());
        if(canJump)
        {
            Jump();
        }

        if(rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity * (fallMultiplier - 1);
        }
        else if (rb.velocity.y > 0 && !holdingJump)
        {
            rb.velocity += Vector2.up * Physics2D.gravity * (lowJumpMultiplier - 1);
        }
    }

    
    private void CheckIfGrounded()
    {
        bool hasGroundCollider = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundChecker.position, checkGroundRadius, canJumpFrom);
        
        foreach (Collider2D collider in colliders)
        {
            if(collider != null && collider.gameObject != this.gameObject)
            {
                hasGroundCollider = true;
                break;
            }
        }

        if(hasGroundCollider) { DoIfGrounded(); }
        else { DoIfNotGrounded(); }
    }

    private void Flip()
    {
        facingRight = !facingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
