using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;
    private float dirx;

    [SerializeField] private LayerMask jumpable;

    [SerializeField] private float speed = 7f;
    [SerializeField] private float jump = 7f;

    private enum Movements
    {
        idle,
        running,
        jumping,
        falling
    }


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        dirx = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(dirx * speed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jump);
        }

        AnimUpdate();
    }

    private void AnimUpdate()
    {
        Movements state;

        if (dirx > 0f)
        {
            state = Movements.running;
            sprite.flipX = false;
        }
        else if (dirx < 0)
        {
            state = Movements.running;
            sprite.flipX = true;
        }
        else
        {
            state = Movements.idle;
        }

        if(rb.velocity.y > .1f)
        {
            state = Movements.jumping;
        }
        else if(rb.velocity.y < -.1f)
        {
            state = Movements.falling;
        }

        anim.SetInteger("State", (int)state);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpable); 
            // "new box" with down offset to box collider
            // box cast returns if there is a collision with the "new box"

            // see component platform effector for one way / two way jumpability
    }
}
