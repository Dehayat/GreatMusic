using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
public class BunnyActiveMode : Node
{

    private GameObject _player;
    private GameObject _bunny;
    private Rigidbody2D _bunnyBody;
    
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
        _bunnyBody = _bunny.GetComponent<Rigidbody2D>();
    }

    public override NodeState Evaluate()
    {
        var velocity = calcBallisticVelocityVector(_bunny.transform.position,_player.transform.position, angle);
        _bunnyBody.AddForce(new Vector2(velocity.x, velocity.y) * _bunnyBody.mass, ForceMode2D.Impulse);
        return NodeState.SUCCESS;
    }
}
