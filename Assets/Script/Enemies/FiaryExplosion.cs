using System;
using System.Collections;
using System.Collections.Generic;
using Rosa;
using UnityEngine;

public class FiaryExplosion : MonoBehaviour
{
    [Header("Damage Values")] [SerializeField]
    private float damageToBeDealt;

    private Combat _combat;
    private GameObject target;
    
    // Start is called before the first frame update
    void Start()
    {
        _combat = GetComponent<Combat>();
        _combat.HitEvent += OnHit;
        target = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnHit(HitInfo info)
    {
        GetComponent<Health>().Damage(info.attackData.data.damage);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.rigidbody.gameObject == target)
        {
            Destroy(gameObject);
        }
    }
}
