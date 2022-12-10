using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float lifeTime = 3f;

    private Rigidbody2D rb;

    private Vector2 velocity;

    public void SetVelocity(Vector2 vel)
    {
        velocity = vel;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        StartCoroutine(DieAfter(lifeTime));
    }

    IEnumerator DieAfter(float t)
    {
        yield return new WaitForSeconds(t);
        Die();
    }
    private void Die()
    {
        Destroy(gameObject);
    }
    private void FixedUpdate()
    {
        rb.velocity = velocity;
    }
}
