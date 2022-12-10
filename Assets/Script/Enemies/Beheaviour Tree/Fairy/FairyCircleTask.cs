using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class FairyCircleTask : Node
{
    public Transform _transform;

    public Vector3 target;

    public float rotationSpeed = 1;

    public float CircleRadius = 1;

    public float ElevationOffset = 0;
    
    private Vector3 positionOffset;
    private float angle = 10f;

    public FairyCircleTask(Transform fairy)
    {

    }
    
    public override NodeState Evaluate()
    {
        
        var state = NodeState.RUNNING;
        return state;
    }
    
}
