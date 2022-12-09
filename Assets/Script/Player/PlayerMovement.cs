using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10f;

    public float jumpSpeed = 30f;
    public float jumpMinDuration = 0.1f;
    public float jumpMaxDuration = 0.5f;

    public float dashSpeed = 30f;
    public float dashDuration = 0.4f;

    public LayerMask groundLayer = 1;

    private Collider2D bodyCollider;
    private Rigidbody2D rb;


    private bool isOnGround = false;
    private Collider2D[] collisionResult = new Collider2D[1];
    private bool canDash = true;

    private void Awake()
    {
        bodyCollider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    public bool GetIsOnGround()
    {
        return isOnGround;
    }

    private float moveValue = 0;
    public void SetMoveSpeed(float moveSpeed)
    {
        moveValue = moveSpeed;
    }

    public void UpdateMovement()
    {
        int moveDir = 0;
        if (moveValue > Mathf.Epsilon)
        {
            moveDir = 1;
        }
        else if (moveValue < -Mathf.Epsilon)
        {
            moveDir = -1;
        }
        Vector2 velocity = rb.velocity;
        velocity.x = moveSpeed * moveDir;
        rb.velocity = velocity;
    }

    public void EnableDash(bool canDash)
    {
        this.canDash = canDash;
    }
    public bool GetCanDash()
    {
        return canDash;
    }


    private void FixedUpdate()
    {
        CheckIsOnGround();
    }

    private void CheckIsOnGround()
    {
        Vector2 origin = bodyCollider.bounds.center + (Vector3.down * (bodyCollider.bounds.extents.y * 0.75f));
        Vector2 size = new Vector2(bodyCollider.bounds.size.x * 0.8f, bodyCollider.bounds.extents.y * 0.75f);
        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.NoFilter();
        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask(groundLayer);
        int collisionCount = Physics2D.OverlapBox(origin, size, 0, contactFilter, collisionResult);
        if (collisionCount != 0)
        {
            isOnGround = true;
        }
        else
        {
            isOnGround = false;
        }
    }
    private void OnDrawGizmos()
    {
        Collider2D col = GetComponent<Collider2D>();
        Vector2 origin = col.bounds.center + (Vector3.down * (col.bounds.extents.y * 0.75f));
        Vector2 size = new Vector2(col.bounds.size.x * 0.8f, col.bounds.extents.y * 0.75f);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(origin, size);

        origin = col.bounds.center + (Vector3.up * (col.bounds.extents.y * 0.75f));
        size = new Vector2(col.bounds.size.x * 0.6f, col.bounds.extents.y * 0.45f);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(origin, size);
    }
}
