using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffects : MonoBehaviour
{
    public GameObject jumpEffect;
    public Transform jumpBasePosition;

    private void OnEnable()
    {
        EventSystem.GetInstance().ListenToEvent("PlayerJump", PlayerJumpEffect);
    }

    private void PlayerJumpEffect(EventData obj)
    {
        Instantiate(jumpEffect, jumpBasePosition.position, Quaternion.identity);
    }

    private void OnDisable()
    {
        EventSystem.GetInstance().IgnoreEvent("PlayerJump", PlayerJumpEffect);
    }
}
