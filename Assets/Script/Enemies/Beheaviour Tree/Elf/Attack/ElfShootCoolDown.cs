using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

public class ElfShootCoolDown : Node
{
    public override NodeState Evaluate()
    {
        //Debug.Log("ShootCool");
        if (Time.time > (float)GetData("shootCoolDown") && (bool)GetData("isShootCooldown"))
        {
            parent.SetData("isShootCooldown", false);
            return NodeState.FAILURE;
        }
        else if (!(bool)GetData("isShootCooldown"))
        {
            return NodeState.FAILURE;
        }

        return NodeState.SUCCESS;
    }
}