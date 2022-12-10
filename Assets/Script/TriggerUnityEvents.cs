using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerUnityEvents : MonoBehaviour
{

    public bool playerOnly = true;
    public bool destroyAfter = true;

    public UnityEvent events;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (playerOnly && (collision.attachedRigidbody == null || !collision.attachedRigidbody.CompareTag("Player")))
        {
            return;
        }
        events?.Invoke();
        if (destroyAfter)
        {
            Destroy(gameObject);
        }
    }
}
