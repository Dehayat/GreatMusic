using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
using Rosa;

public class FairyBT : BehaviorTree.Tree
{

    [Header("Active Mode")] [SerializeField] private int DistanceForActiveMode;

    [Header("Attack")] [SerializeField] private int DistanceForExplosion;
    
    private GameObject _player;

    public void Awake()
    {
        _player = GameObject.FindWithTag("Player");
    }

    protected override Node SetupTree()
    {
        Debug.Log("Here" + transform.position);
        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new FiaryCheckIfPlayerInRange(_player, transform, DistanceForActiveMode),
                new FairyActiveMode(GetComponent<FiaryActiveBeahiouv>(), GetComponent<CirclePath>(), transform)
            }),
            new FairyCircleTask(transform),
        });
        root.SetData("PassivePositionTarget", transform.position);
        return root;
    }
    
}
