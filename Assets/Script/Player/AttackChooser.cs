using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackChooser : MonoBehaviour
{

    public PlayerAttack usedAttack;
    public PlayerAttack GetCurrentAttack()
    {
        return usedAttack;
    }

    internal void SwitchWeapon()
    {
        var allWeapons = GetComponentsInChildren<PlayerAttack>();
        for (int i = 0; i < allWeapons.Length; i++)
        {
            if (allWeapons[i] != usedAttack)
            {
                usedAttack = allWeapons[i];
                break;
            }
        }
    }
}
