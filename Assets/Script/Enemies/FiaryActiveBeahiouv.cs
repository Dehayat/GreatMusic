using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiaryActiveBeahiouv : MonoBehaviour
{
    [Header("Movement Variables")] [SerializeField]
    private float speed = 1.0f;

    [Header("CoolDown Timers")] [SerializeField]
    private float movementTime = 2.0f;

    [SerializeField] private float waitTime = 1.0f;
    private float nextWait = 0f;
    private float waitTimer = 0f;
    private bool waiting = false;
    
    private Transform target;

    private void Awake()
    {
        target = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnEnable()
    {
        nextWait = Time.time + waitTime;
    }

    private void FixedUpdate()
    {
        //Debug.Log("CurrentTime:" + Time.time + "NextWait" + nextWait);
        if (waiting)
        {
            if (Time.time > waitTimer) waiting = false;
            nextWait = Time.time + waitTime;
            return;
        }

        if (Time.time < nextWait)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            waitTimer = Time.time + movementTime;
            return;
        }
        waiting = true;
    }
}