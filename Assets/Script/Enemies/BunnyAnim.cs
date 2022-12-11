using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyAnim : MonoBehaviour
{
    public int frameUpdate = 3;

    private Rigidbody2D rb;
    private Animator anim;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (Time.frameCount % frameUpdate != 0)
        {
            return;
        }
        if (rb.velocity.y > 0.1f)
        {
            anim.SetBool("Jump", true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.position.y < transform.position.y)
        {
            anim.SetBool("Jump", false);
        }
    }
}
