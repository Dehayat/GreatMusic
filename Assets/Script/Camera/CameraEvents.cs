using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEvents : MonoBehaviour
{
    private void OnEnable()
    {
        EventSystem.GetInstance().ListenToEvent("TeleportPlayer", SnapToPlayer);
    }

    private void SnapToPlayer(EventData eventData)
    {
        GetComponent<FollowCamera>().SnapToTarget();
    }

    private void OnDisable()
    {
        EventSystem.GetInstance().IgnoreEvent("TeleportPlayer", SnapToPlayer);
    }
}
