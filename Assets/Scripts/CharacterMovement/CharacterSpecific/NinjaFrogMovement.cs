using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaFrogMovement : PlayerMovement
{

    [SerializeField] private int additionalJumps = 1;
    private int jumpsLeft;
    private bool isDoubleJump;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Animate()
    {
        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        animator.SetBool("IsJumping", rb.velocity.y > 0.02);
        animator.SetBool("IsFalling", rb.velocity.y < -0.02);
        animator.SetBool("IsDoubleJump", isDoubleJump);
    }

    protected override void Jump()
    {
        if(!(isGrounded || Time.time - lastTimeGrounded <= rememberGroundedFor))
        {
            isDoubleJump = true;
            jumpsLeft -= 1;
        }
        base.Jump();
    }

    protected override bool CanJump()
    {
        return jumpsLeft > 0;
    }

    protected override void DoIfGrounded()
    {
        base.DoIfGrounded();
        jumpsLeft = additionalJumps;
    }

    protected override void DoIfNotGrounded()
    {
        if(isGrounded)
        {
            lastTimeGrounded = Time.time;
            isDoubleJump = false;
        }
        isGrounded = false;
    }
}
