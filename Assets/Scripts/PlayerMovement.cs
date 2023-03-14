using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private float horizontal;
    public float playerSpeed = 5;
    public float jumpForce = 6.5F;
    public bool isGrounded;
    public LayerMask groundLayerMask;
    public float raycastLength = 2;
    private SpriteRenderer spriteRenderer;
    private Animator anim;
    public GameObject other;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(horizontal * playerSpeed, rb.velocity.y);
        horizontal = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x,jumpForce);
        }
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, raycastLength, groundLayerMask);
        Debug.DrawRay(transform.position, Vector3.down * raycastLength, Color.green);
        anim.SetBool("isGrounded", isGrounded);

        if (rb.velocity.x != 0)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
        if (horizontal<0)
        {
            spriteRenderer.flipX = true;
        }
        else if (horizontal>0)
        {
            spriteRenderer.flipX = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "coin")
        {
            Destroy(other.gameObject);
        }
    }
}