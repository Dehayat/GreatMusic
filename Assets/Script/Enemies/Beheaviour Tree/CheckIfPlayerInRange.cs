using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

using BehaviorTree;

public class CheckIfPlayerInRange : Node
{

    private GameObject _player;
    private Transform _myPosition;

    private int distanceToCheck;
    
    public CheckIfPlayerInRange(GameObject player, Transform myPosition, int distanceToCheck)
    {
        _player = player;
        _myPosition = myPosition;
        this.distanceToCheck = distanceToCheck;
    }

    public override NodeState Evaluate()
    {
        float distanceToPlayer = Vector3.Distance(_player.transform.position, _myPosition.position);
        
        if (distanceToPlayer < distanceToCheck)
        {
            return NodeState.SUCCESS;
        }
        return NodeState.FAILURE;
    }
}
