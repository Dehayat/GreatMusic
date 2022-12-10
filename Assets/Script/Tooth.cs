using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tooth : MonoBehaviour
{
    public int teeth;

    public void Interact()
    {
        Debug.Log("Interacted");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.attachedRigidbody != null && other.attachedRigidbody.CompareTag("Player"))
        {
            FindObjectOfType<GameData>().teeth += teeth;
            Destroy(gameObject);
        }
    }
}
