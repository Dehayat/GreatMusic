using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

public class CheckIfMoving : Node
{

    private Rigidbody2D bunnyBody;

    public CheckIfMoving(Rigidbody2D bunnyBody)
    {
        this.bunnyBody = bunnyBody;
    }

    public override NodeState Evaluate()
    {
        if (bunnyBody.velocity != Vector2.zero)
        {
            return NodeState.SUCCESS;
        }

        return NodeState.FAILURE;
    }
}
