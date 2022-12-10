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
        _transform = fairy;
        target = _transform.position;
    }
    
    public override NodeState Evaluate()
    {
        positionOffset.Set(
            Mathf.Cos( angle ) * CircleRadius,
            Mathf.Sin( angle ) * CircleRadius,
            0
            
        );
        _transform.position = target + positionOffset;
        angle += Time.deltaTime * rotationSpeed;
        
        
        var state = NodeState.RUNNING;
        return state;
    }
    
}
