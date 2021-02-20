using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_script : MonoBehaviour
{ 
    public float speed;
    private Rigidbody2D rb;
    private float moveInput;

    public float jumpForce;
    private bool grounded;
    public Transform feet;
    public float circleRadius;
    public LayerMask isGround;

    public float jumpTime;
    private float jumpTimeCounter;
    private bool isJumping;


  void Start()
  {
    rb = GetComponent<Rigidbody2D>();
  }

  void Update()
  {
    grounded = Physics2D.OverlapCircle(feet.position, circleRadius);
    if (grounded == true && Input.GetKeyDown(KeyCode.Space))
    {
        isJumping = true;
        jumpTimeCounter = jumpTime;
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }
    if (isJumping == true && Input.GetKey(KeyCode.Space))
    {
        if (jumpTimeCounter > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpTimeCounter -= Time.deltaTime;
        }
        else if (jumpTimeCounter < 0)
            isJumping = false;
    }
    if (Input.GetKeyUp(KeyCode.Space))
        isJumping = false;
  }

  void FixedUpdate()
  {
    moveInput = Input.GetAxis("Horizontal");
    if (moveInput > 0)
    {
        transform.eulerAngles = Vector3.zero;
    }
    else if (moveInput < 0)
    {
        transform.eulerAngles = new Vector3(0, 180);
    }
    rb.velocity = new Vector2(speed * moveInput, rb.velocity.y);
  }
}
