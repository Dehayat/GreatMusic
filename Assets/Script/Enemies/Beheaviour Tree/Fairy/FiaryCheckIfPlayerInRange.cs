using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

using BehaviorTree;

public class FiaryCheckIfPlayerInRange : Node
{

    private GameObject _player;
    private Transform _fairyPosition;

    private int distanceToCheck;
    
    public FiaryCheckIfPlayerInRange(GameObject player, Transform fairyPosition, int distanceToCheck)
    {
        _player = player;
        _fairyPosition = fairyPosition;
        this.distanceToCheck = distanceToCheck;
    }

    public override NodeState Evaluate()
    {
        float distanceToPlayer = Vector3.Distance(_player.transform.position, _fairyPosition.position);
        
        if (distanceToPlayer < distanceToCheck)
        {
            return NodeState.SUCCESS;
        }
        return NodeState.FAILURE;
    }
}
