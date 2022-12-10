using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class BunnyPassiveMode : Node
{

    [Header("JumpVariables")] 
    [SerializeField] private float distanceToJump;
    [SerializeField] private float angle;
    
    private GameObject _bunny;
    private Rigidbody2D bunnyBody;

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

    public BunnyPassiveMode(float distanceToJump, float angle, GameObject bunny)
    {
        this.distanceToJump = distanceToJump;
        this.angle = angle;
        _bunny = bunny;
        bunnyBody = _bunny.GetComponent<Rigidbody2D>();
    }

    public override NodeState Evaluate()
    {
        if (bunnyBody.velocity == Vector2.zero)
        {
            Vector3 newposition = new Vector3(_bunny.transform.position.x + distanceToJump, _bunny.transform.position.y,
                _bunny.transform.position.z);
            var velocity = calcBallisticVelocityVector(_bunny.transform.position, newposition, angle);
            bunnyBody.AddForce(new Vector2(velocity.x, velocity.y) * bunnyBody.mass, ForceMode2D.Impulse);
        }

        var state = NodeState.RUNNING;
        return state;
    }
}
