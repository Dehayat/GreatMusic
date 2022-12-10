using System.Collections;
using System.Collections.Generic;
using Rosa;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    private Health enemy_health;
    private Combat enemy_combat;
    
    // Start is called before the first frame update
    void Start()
    {
        enemy_health = GetComponent<Health>();
        enemy_combat = GetComponent<Combat>();
        enemy_combat.HitEvent += onHit;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onHit(HitInfo info)
    {
        Debug.Log("Here");
        enemy_health.Damage(info.attackData.data.damage);
        if (enemy_health.GetHealth() <= 0)
        {
            Debug.Log("EnemyController");
            EventSystem.GetInstance().EmitEvent("ScoreEvent", new ScoreAddEvent(10f));
            Destroy(gameObject);
        }
    }
    
    
}
