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
}
