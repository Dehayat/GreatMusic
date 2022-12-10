using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class FairyBT : BehaviorTree.Tree
{

    protected override Node SetupTree()
    {
        Debug.Log("Here" + transform.position);
        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new FiaryCheckIfPlayerInRange(),
                new FairyPassiveMode()
            }),
            new FairyCircleTask(transform),
        });
        return root;
    }
    
}
