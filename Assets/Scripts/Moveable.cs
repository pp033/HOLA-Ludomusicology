using UnityEngine;

public class Moveable : MonoBehaviour
{
    private float dirx;

    private bool crushed = false;

    private GameObject cam;
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;

    [SerializeField] private AudioClip audioclipJump;

    [SerializeField] private LayerMask jumpable;

    [SerializeField] private float speed = 7f;
    [SerializeField] public float jump = 7f;

    public GameObject floor { get; private set; }

    private enum Movements
    {
        idle,
        running,
        jumping,
        falling
    }

    private void Start()
    {
        cam = GameObject.Find("Main Camera");
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if(IsCrushed() && !crushed)
        {
            GetComponent<Lives>().Die();
            crushed = true;
        }

        dirx = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(dirx * speed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            AudioSource.PlayClipAtPoint(audioclipJump, cam.transform.position);
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
            // NOTE: returns if there is a collision with a "new box" with offset to box collider
    }

    private bool IsCrushed()
    {
        Vector3 screenPoint = cam.GetComponent<Camera>().WorldToViewportPoint(sprite.bounds.center);
        bool visible = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;

        if (!visible)
        {
            return true;
        }
        return false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(IsGrounded() && collision.gameObject.CompareTag("Platform"))
        {
            floor = collision.gameObject;
        }
        else
        {
            floor = null;
        }
    }
}
