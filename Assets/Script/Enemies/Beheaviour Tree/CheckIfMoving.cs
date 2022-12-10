using BehaviorTree;
using UnityEngine;

public class CheckIfMoving : Node
{
    private Collider2D bodyCollider;
    private Rigidbody2D bunnyBody;
    private Transform _position;

    public CheckIfMoving(Rigidbody2D bunnyBody, Collider2D col, Transform position)
    {
        this.bunnyBody = bunnyBody;
        bodyCollider = col;
        _position = position;
    }

    //TODO fix this
    private bool CheckIsOnGround()
    {
        var raycastDistance = 0.1f;
        var hit = Physics2D.Raycast(_position.position, -Vector2.up, raycastDistance, layerMask: LayerMask.NameToLayer("Ground"));
        if (hit.collider != null && hit.transform.gameObject.layer == LayerMask.NameToLayer("Ground"))
            return true;
        return false;
    }


    public override NodeState Evaluate()
    {
        if (bunnyBody.velocity != Vector2.zero) return NodeState.SUCCESS;

        return NodeState.FAILURE;
    }
}