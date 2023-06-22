using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D bc;
    private Animator anim;
    private SpriteRenderer sprite;

    [SerializeField] private LayerMask JumpableGround;

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;
    [SerializeField] private float slideSpeed = 0.5f;
    [SerializeField] private AudioSource JumpSound;
    [SerializeField] private AudioSource DeadSound;
    [SerializeField] private ParticleSystem ParticleSystem;

    private bool doubleJump;
    private bool wallJump;
    private bool wallSliding;

    public int CollectedCollectibles;

    private enum MovementState { idle, running, jumping, falling, doubleJump, wallSlide };

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        CollectedCollectibles = 0;
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if (IsGrounded())
        {
            wallJump = true;
            wallSliding = false;
            if (!Input.GetButton("Jump")) doubleJump = false;
        } else
        {
            if (!IsStickLeft() && !IsStickRight()) wallSliding = false;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if ((IsStickLeft() || IsStickRight()) && wallJump && !IsGrounded())
            {
                JumpSound.Play();
                ParticleSystem.Play();
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                wallJump = !wallJump;
            }
            else if (IsGrounded() || doubleJump && wallJump)
            {
                JumpSound.Play();
                ParticleSystem.Play();
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                doubleJump = !doubleJump;
            }

            wallSliding = false;
        }

        if (wallSliding) rb.velocity = new Vector2(rb.velocity.x, -slideSpeed);

        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        MovementState State;

        if (dirX > 0f)
        {
            ParticleSystem.Play();
            State = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            ParticleSystem.Play();
            State = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            State = MovementState.idle;
        }

        if (!IsGrounded())
        {
            if (rb.velocity.y > .1f)
            {
                State = MovementState.jumping;

                if (!doubleJump)
                {
                    State = MovementState.doubleJump;
                }

                wallSliding = false;
            }
            else if (rb.velocity.y < -.1f)
            {
                State = MovementState.falling;

                if (IsStickLeft() || IsStickRight()) wallSliding = true;
                else wallSliding = false;
            }

            if (IsStickLeft())
            {
                State = MovementState.wallSlide;
                sprite.flipX = true;
            }
            else if (IsStickRight())
            {
                State = MovementState.wallSlide;
                sprite.flipX = false;
            }

            if (rb.velocity.y > .1f && !wallJump)
            {
                State = MovementState.doubleJump;
            }
        }

        anim.SetInteger("State", (int)State);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Collectible"))
        {
            CollectedCollectibles++;
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, 0f, Vector2.down, .1f, JumpableGround);
    }

    private bool IsStickLeft()
    {
        return Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, 0f, Vector2.left, .1f, JumpableGround);
    }

    private bool IsStickRight()
    {
        return Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, 0f, Vector2.right, .1f, JumpableGround);
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}