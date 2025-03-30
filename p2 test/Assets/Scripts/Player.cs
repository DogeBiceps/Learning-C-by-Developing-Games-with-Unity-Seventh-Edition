using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;


public class Player : MonoBehaviour
{
    public SpriteRenderer playerSprite;
    public Rigidbody2D rb;
    public float moveSpeed = 5f;
    float horizontalMovement;

    public float jumpForce = 10f;

    public Transform groundCheckPos;
    public Vector2 groundCheckSize = new Vector2(0.5f, 0.05f);
    public LayerMask groundLayer;

    public float baseGravity = 2f;
        public float maxFallSpeed = 18f;
    public float fallSpeedMultiplier = 2f;

    [SerializeField]
    public bool isDead;

    private void Start()
    {
        isDead = false;
    }
    private void FixedUpdate()
    {
        rb.linearVelocityX = (horizontalMovement * moveSpeed);
        isGrounded();
        Gravity();
    }
    public void Move(InputAction.CallbackContext context)
    {
        horizontalMovement = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (isGrounded())
        {
            if (context.performed)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpForce);
            }
            else if (context.canceled)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpForce * 0.5f);
            }
        }
    }

    private bool isGrounded()
    {
        if(Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, groundLayer))
        {
            return true;
        }
        return false;
    }

    private void Gravity()
    {
        if(rb.linearVelocityY < 0)
        {
            rb.gravityScale = baseGravity * fallSpeedMultiplier;
            rb.linearVelocity = new Vector2(rb.linearVelocityX, Mathf.Max(rb.linearVelocityY, -maxFallSpeed));
        }
        else
        {
            rb.gravityScale = baseGravity;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(groundCheckPos.position, groundCheckSize);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
            if (other.gameObject.CompareTag("Evil"))
            {
                isDead = true;
                playerSprite.enabled = false;
        }
            else
            {
                return;
            }
    }
}