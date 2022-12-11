using System.Collections;
using System.Collections.Generic;
using Rosa;
using UnityEngine;
using Tree = BehaviorTree.Tree;


public class EnemyController : MonoBehaviour
{

    private Health enemy_health;
    private Combat enemy_combat;
    [SerializeField] private ToothType dropToothType;
    [SerializeField] private float DeathTimer;
    private float DeathCoolDown;
    public bool Dead = false;
    [SerializeField] private Tree beheaviourTree;
    
    
    // Start is called before the first frame update
    void Start()
    {
        enemy_health = GetComponent<Health>();
        enemy_combat = GetComponent<Combat>();
        enemy_combat.HitEvent += onHit;
        beheaviourTree = GetComponent<Tree>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Dead)
        {
            if(Time.time > DeathCoolDown) Destroy(gameObject);
        }
    }

    public void onHit(HitInfo info)
    {   
        enemy_health.Damage(info.attackData.data.damage);
        if (enemy_health.GetHealth() <= 0)
        {
            EventSystem.GetInstance().EmitEvent("DropTeeth", new TeethEvent(gameObject.transform.position, dropToothType));
            EventSystem.GetInstance().EmitEvent("ScoreEvent", new ScoreAddEvent(10f));
            Dead = true;
            DeathCoolDown = Time.time + DeathTimer;
            beheaviourTree.enabled = false;
            GetComponent<Animator>().SetBool("Death", true);
        }
    }
    
    
}
