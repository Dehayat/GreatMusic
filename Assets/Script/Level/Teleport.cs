using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform destination;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.attachedRigidbody != null && collision.attachedRigidbody.CompareTag("Player"))
        {
            GameObject.FindGameObjectWithTag("Player").transform.position = destination.position;
            EventSystem.GetInstance().EmitEvent("TeleportPlayer", null);
        }
    }
}
