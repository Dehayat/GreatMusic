using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

public class ElfRepeat : Node
{
    public override NodeState Evaluate()
    {
        //Debug.Log("Repeat");
        if (Time.time > (float)GetData("TeleportCoolDown"))
        { 
            parent.SetData("isShootCooldown", false);
            parent.SetData("isTeleportCooldown", false);
            parent.SetData("shootingCounter", 0);
        }
        return NodeState.SUCCESS;
    }
}
