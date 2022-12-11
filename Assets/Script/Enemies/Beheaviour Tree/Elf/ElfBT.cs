using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
using UnityEngine.Serialization;
using Tree = BehaviorTree.Tree;

public class ElfBT : Tree
{
    [Header("Active Mode")] [SerializeField] private int DistanceForActiveMode = 10;
    [SerializeField] private float postTeleportCoolDown = 3f;

    [Header("Attack Mode")] [SerializeField]
    private int AmountOfShots = 5;


    [Header("Shooting")] [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed = 30f;
    [SerializeField] private float lifeTimeBullet = 3f;
    [SerializeField] private float timeBetweenShots = 0.5f;
    
    [Header("Teleporting")]
    [SerializeField] private float xDistanceTeleport = 2f;
    [SerializeField] private float yDistanceTeleport = 2f;
    [SerializeField] private float limitDown;
    [SerializeField] private float limitLeft;
    [SerializeField] private float limitRight;
    [SerializeField] private float limitUp;
    
    private GameObject _player;
    
    public void Awake()
    {
        _player = GameObject.FindWithTag("Player");
    }

    
    // Start is called before the first frame update
    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new CheckIfPlayerInRange(_player, transform, DistanceForActiveMode),
                new Selector(new List<Node>
                {
                    new ElfShoot(timeBetweenShots, AmountOfShots, bulletPrefab, gameObject, _player, bulletSpeed, lifeTimeBullet),
                    new ElfShootCoolDown(),
                    new ElfTeleport(postTeleportCoolDown, xDistanceTeleport, yDistanceTeleport, _player.transform, transform, limitLeft, limitRight, limitUp, limitDown),
                    new ElfRepeat()
                })
            }),
            new ElfPassiveMode(),
        });
        root.SetData("PassivePositionTarget", transform.position);
        return root;
    }
}
