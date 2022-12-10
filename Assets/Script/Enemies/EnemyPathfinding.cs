using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{

    [Header("Movement Variables")] [SerializeField]
    private float speed = 1.0f;
    
    private GameObject player;
    private Vector3 target = new Vector3(0.0f, 0.0f, 0.0f);
    
    
    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        target = findPlayer();
        //Debug.Log("Target" + target.ToString());
        //Debug.Log("Current Position" + transform.position);
        moveToTarget();
    }

    private Vector3 findPlayer()
    {
        return player.GetComponent<Player>().GetPosition();
    }


    private void moveToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, target,speed * Time.deltaTime);
    }
}
