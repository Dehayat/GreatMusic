using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DieOnCollide : MonoBehaviour
{
    public UnityEvent dieEvents;

    private bool dead = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (dead) return;
        dead = true;
        dieEvents?.Invoke();
        Destroy(gameObject);
    }
}
