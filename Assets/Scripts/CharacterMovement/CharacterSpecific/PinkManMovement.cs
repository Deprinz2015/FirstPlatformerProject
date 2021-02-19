using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkManMovement : PlayerMovement
{
    [SerializeField] private float grabDistance = 1f;
    [SerializeField] private LayerMask boxMask;
    private GameObject grabbedBox;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    protected override void CustomUpdate()
    {
        Physics2D.queriesStartInColliders = false;
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, this.transform.localScale * Vector2.right, grabDistance, boxMask);
        
        if(hit.collider != null && Input.GetKeyDown(KeyCode.G))
        {
            grabbedBox = hit.collider.gameObject;
            FixedJoint2D boxJoint = grabbedBox.GetComponent<FixedJoint2D>();
            boxJoint.enabled = true;
            boxJoint.connectedBody = this.rb;
        }
        else if(grabbedBox != null && Input.GetKeyUp(KeyCode.G))
        {
            FixedJoint2D boxJoint = grabbedBox.GetComponent<FixedJoint2D>();
            boxJoint.enabled = false;
            grabbedBox = null;
        }
    }

    protected override void Animate()
    {
        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        animator.SetBool("IsJumping", rb.velocity.y > 0.02);
        animator.SetBool("IsFalling", rb.velocity.y < -0.02);
    }
}
