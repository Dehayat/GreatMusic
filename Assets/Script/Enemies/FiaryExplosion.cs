using System;
using System.Collections;
using System.Collections.Generic;
using Rosa;
using UnityEngine;

public class FiaryExplosion : MonoBehaviour
{
    private Combat _combat;
    private GameObject target;
    [SerializeField] private float ExplosionTimer;
    [SerializeField] private EnemyController Fairycontroller;

    private float ExpCoolDown;
    private bool Exploded = false;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player");
        Fairycontroller = GetComponent<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Exploded)
        {
            if (Time.time > ExpCoolDown) Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.rigidbody.gameObject == target && Fairycontroller.Dead == false)
        {
            Exploded = true;
            ExpCoolDown = Time.time + ExplosionTimer;
            GetComponent<Animator>().SetBool("Exploded", true);
            
        }
    }
}
