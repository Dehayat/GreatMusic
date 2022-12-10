using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using UnityEngine.Serialization;
using Tree = BehaviorTree.Tree;

public class BunnyBT : Tree
{
    [Header("Active Mode")] [SerializeField] private int DistanceForActiveMode;

    [Header("Passive Mode")] [SerializeField]
    private float DistanceToJump;
    [SerializeField]
    private float angle;
    
    private GameObject _player;
    
    // Start is called before the first frame update
    public void Awake()
    {
        _player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    protected override Node SetupTree()
    {
        Debug.Log("Here" + transform.position);
        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new CheckIfPlayerInRange(_player, transform, DistanceForActiveMode),
                new BunnyActiveMode(_player, gameObject, angle)
            }),
            new BunnyPassiveMode(DistanceToJump, angle, gameObject),
        });
        root.SetData("PassivePositionTarget", transform.position);
        return root;
    }
}
