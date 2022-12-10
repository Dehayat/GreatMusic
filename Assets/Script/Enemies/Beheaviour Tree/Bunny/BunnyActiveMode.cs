using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
public class BunnyActiveMode : Node
{

    private GameObject _player;
    private GameObject _bunny;
    
    //TODO: maybe we can calculate this later
    private float angle = 15f;
    
    
    private Vector3 calcBallisticVelocityVector(Vector3 source, Vector3 target, float angle)
    {
        Vector3 direction = target - source;                            
        float h = direction.y;                                           
        direction.y = 0;                                               
        float distance = direction.magnitude;                           
        float a = angle * Mathf.Deg2Rad;                                
        direction.y = distance * Mathf.Tan(a);                            
        distance += h/Mathf.Tan(a);                                      
 
        // calculate velocity
        float velocity = Mathf.Sqrt(distance * Physics.gravity.magnitude / Mathf.Sin(2*a));
        return velocity * direction.normalized;    
    }

    public BunnyActiveMode(GameObject player, GameObject bunny, float angle)
    {
        _player = player;
        _bunny = bunny;
        this.angle = angle;
    }

    public override NodeState Evaluate()
    {
        Debug.Log(("Active"));
        Rigidbody2D bunnyBody = _bunny.GetComponent<Rigidbody2D>();
        if (bunnyBody.velocity == Vector2.zero)
        {
            var velocity = calcBallisticVelocityVector(_bunny.transform.position,_player.transform.position, angle);
            bunnyBody.AddForce(new Vector2(velocity.x, velocity.y) * bunnyBody.mass, ForceMode2D.Impulse); 
        }
        return NodeState.SUCCESS;
    }
}
