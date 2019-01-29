using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float moveSpeed = 3.0f;
    public float jumpCooldown = 1.0f;
    public float jumpForce = 10.0f;
    public float forceScale = 10.0f;
    private float lastJump;
    private Rigidbody2D rb;
    public LayerMask walkable;
    public bool isGrounded = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lastJump = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        GroundCheck();
        if (isGrounded) { rb.gravityScale = 0.0f; } else { rb.gravityScale = 1.0f; }
        Vector2 moveDir = Vector2.zero;
        if (Input.GetKey(KeyCode.A))
        {
            moveDir.x -= 1.0f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveDir.x += 1.0f;
        }
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > lastJump+ jumpCooldown && isGrounded)
        {
            Jump();
        }


        rb.AddForce(moveDir * (Mathf.Clamp((moveSpeed - rb.velocity.magnitude), 0.0f, forceScale*moveSpeed) * forceScale));
    }

    private void GroundCheck()
    {
        RaycastHit2D hits = Physics2D.BoxCast(this.transform.position, Vector2.one*0.3f, 0.0f, Vector2.down, 0.1f, walkable, 0.0f);
        if (hits){
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    void Jump()
    {
        lastJump = Time.time;
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
}
