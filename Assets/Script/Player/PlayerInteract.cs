using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private void OnEnable()
    {
        EventSystem.GetInstance().ListenToEvent("CanInteract", CanInteract);
        EventSystem.GetInstance().ListenToEvent("CantInteract", CantInteract);
    }

    private Interactable currentInteractable;

    private void CanInteract(EventData obj)
    {
        var intercatData = obj as InteractableEvent;
        currentInteractable = intercatData.interactable;
    }

    private void CantInteract(EventData obj)
    {
        var intercatData = obj as InteractableEvent;
        if (currentInteractable != null && currentInteractable == intercatData.interactable)
        {
            currentInteractable = null;
        }
    }

    private void OnDisable()
    {
        EventSystem.GetInstance().IgnoreEvent("CanInteract", CanInteract);
        EventSystem.GetInstance().IgnoreEvent("CantInteract", CantInteract);
    }

    public void InteractWithCurrent()
    {
        if (currentInteractable != null)
        {
            currentInteractable.Interact();
            currentInteractable = null;
        }
    }

}
