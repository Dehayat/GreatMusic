using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class FairyActiveMode : Node
{

    private FiaryActiveBeahiouv _pathfinding;
    private CirclePath _circlePath;
    private Transform positionFairy;
    
    public FairyActiveMode(FiaryActiveBeahiouv pathfinding, CirclePath circlePath, Transform positionFairy)
    {
        _pathfinding = pathfinding;
        this.positionFairy = positionFairy;
        _circlePath = circlePath;
    }

    public override NodeState Evaluate()
    {
        _pathfinding.enabled = true;
        _circlePath.enabled = false;
        return NodeState.SUCCESS;
        
    }
}
