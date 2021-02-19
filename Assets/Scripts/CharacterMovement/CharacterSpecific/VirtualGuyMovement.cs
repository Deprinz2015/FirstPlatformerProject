using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualGuyMovement : PlayerMovement
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    protected override void Animate()
    {
        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        animator.SetBool("IsJumping", rb.velocity.y > 0.02);
        animator.SetBool("IsFalling", rb.velocity.y < -0.02);
    }
}
