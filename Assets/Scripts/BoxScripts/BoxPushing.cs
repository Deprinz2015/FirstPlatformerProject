using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (Rigidbody2D))]
[RequireComponent(typeof (FixedJoint2D))]
public class BoxPushing : MonoBehaviour
{
    private float mass;
    private Rigidbody2D rb;
    private FixedJoint2D joint2D;

    // Start is called before the first frame update
    void Start()
    {
        this.rb = GetComponent<Rigidbody2D>();
        this.mass = rb.mass;
        this.joint2D = GetComponent<FixedJoint2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(joint2D.enabled)
        {
            rb.mass = mass;
        }
        else
        {
            rb.mass = mass * 1000;
        }
    }
}
