using UnityEngine;

public class SidewaysMover : MonoBehaviour
{
    public float moveSpeed = 3f;
    public Vector2 direction = Vector2.right;

    private Rigidbody2D rb;

    public Transform wallCheckPos;
    public Vector2 wallCheckSize = new Vector2(0.5f, 0.05f);
    public LayerMask GroundLayer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = direction.normalized * moveSpeed;
    }

    void Update()
    {
        FlipDirection();
    }

    public void FlipDirection()
    {
        if (isTouchingWall())
        {
            direction *= -1;
        }
        else
        {
            return;
        }
    
    }



    private bool isTouchingWall()
    {
        if (Physics2D.OverlapBox(wallCheckPos.position, wallCheckSize, 0, GroundLayer))
        {
            return true;
        }
        return false;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(wallCheckPos.position, wallCheckSize);
    }
}
