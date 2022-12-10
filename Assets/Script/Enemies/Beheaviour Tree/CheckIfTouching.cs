using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
using Rosa;

public class CheckIfTouching : Node
{

    private Combat ownCombat;
    private GameObject target;

    private bool touched = false;
    private bool firstRun = true;

    public CheckIfTouching(Combat ownCombat, GameObject target)
    {
        this.ownCombat = ownCombat;
        this.target = target;
    }

    

    private void OnHit(HitInfo info)
    {
        Debug.Log("Hit Registered in Fairy");
        if (info.attacker.GetOwner() == target)
        {
            touched = true;
        }
    }
    
    public override NodeState Evaluate()
    {
        if (firstRun)
        {
            ownCombat.HitEvent += OnHit;
            firstRun = false;
        }
        
        //Debug.Log("IsTouching" + (touched ? NodeState.SUCCESS : NodeState.FAILURE));
        return touched ? NodeState.SUCCESS : NodeState.FAILURE;
    }
    
    
}
