using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class InteractableEvent : EventData
{
    public Interactable interactable;
}

public class Interactable : MonoBehaviour
{
    public void Interact()
    {
        Debug.Log("Interacted");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.attachedRigidbody != null && other.attachedRigidbody.CompareTag("Player"))
        {
            EventSystem.GetInstance().EmitEvent("CanInteract", new InteractableEvent { interactable = this });
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.attachedRigidbody != null && other.attachedRigidbody.CompareTag("Player"))
        {
            EventSystem.GetInstance().EmitEvent("CantInteract", new InteractableEvent { interactable = this });
        }
    }
}
