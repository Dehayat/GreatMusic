using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using UnityEngine.Serialization;
using Tree = BehaviorTree.Tree;

public class BunnyBT : Tree
{
    [Header("Active Mode")] [SerializeField] private int DistanceForActiveMode = 5;

    [Header("Passive Mode")] [SerializeField]
    private float DistanceToJump = 5f;
    [SerializeField]
    private float angle = 45f;

    [Header("Retreat")] [SerializeField] private float distanceWhenToRetreat = 0.9f;
    [SerializeField]
    private float RetreatAngle = 45f;
    
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
            new CheckIfMoving(gameObject.GetComponent<Rigidbody2D>()),
            new Sequence(new List<Node>
            {
                new CheckIfPlayerInRange(_player, transform, distanceWhenToRetreat),
                new BunnyRetreat(5, RetreatAngle, gameObject, _player)
            }),
            new Sequence(new List<Node>
            {
                new CheckIfPlayerInRange(_player, transform, DistanceForActiveMode),
                new BunnyActiveMode(_player, gameObject, angle)
            }),
            new BunnyPassiveMode(DistanceToJump, angle, gameObject, _player),
        });
        root.SetData("PassivePositionTarget", transform.position);
        return root;
    }
}
