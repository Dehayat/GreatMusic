using System;
using System.Collections;
using System.Collections.Generic;
using Rosa;
using UnityEngine;

public class FiaryExplosion : MonoBehaviour
{
    private Combat _combat;
    private GameObject target;
    
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.rigidbody.gameObject == target)
        {
            Destroy(gameObject);
        }
    }
}
