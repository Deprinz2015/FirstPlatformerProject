using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (PlayerMovement))]
public class PlayerControl : MonoBehaviour
{
    [SerializeField] private PlayerMovement movement;
    private bool jump = false;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(!jump)
        {
            jump = Input.GetButtonDown("Jump");
        }
    }

    void FixedUpdate()
    {
        float move = Input.GetAxis("Horizontal");
        movement.Move(move, jump, Input.GetButton("Jump"));
        jump = false;
    }
}
