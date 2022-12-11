using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DieOnCollide : MonoBehaviour
{
    public UnityEvent dieEvents;

    private bool dead = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (dead) return;
        dead = true;
        dieEvents?.Invoke();
        Destroy(gameObject, 0.05f);
    }
}
