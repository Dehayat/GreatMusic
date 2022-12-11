using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
public class ElfPassiveMode : Node
{

    public override void initialize()
    {
        SetDataRoot("Awake", false);
    }

    public override NodeState Evaluate()
    {
        if ((bool)GetData("Awake")) return NodeState.FAILURE;
        return NodeState.RUNNING;
    }
}
