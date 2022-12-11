using BehaviorTree;
using UnityEngine;

public class ElfTeleport : Node
{
    private readonly Transform _elfPos;
    
    private float _limitDown;
    private float _limitLeft;
    private float _limitRight;
    private float _limitUp;

    private readonly Transform _playerPos;
    private readonly float _teleportTimer;
    private readonly float _xDistanceTeleport;
    private readonly float _yDistanceTeleport;

    public ElfTeleport(float TeleportCoolDown, float xDistanceTeleport, float yDistanceTeleport, Transform playerPos,
        Transform elfPos, float limitLeft, float limitRight, float limitUp, float limitDown)
    {
        _teleportTimer = TeleportCoolDown;
        _xDistanceTeleport = xDistanceTeleport;
        _yDistanceTeleport = yDistanceTeleport;

        _playerPos = playerPos;
        _elfPos = elfPos;

        _limitLeft = limitLeft;
        _limitRight = limitRight;
        _limitUp = limitUp;
        _limitDown = limitDown;
    }

    public override void initialize()
    {
        parent.SetData("TeleportCoolDown", 0);
        parent.SetData("isTeleportCooldown", false);
    }

    private void startTeleportTimer(float cooldown)
    {
        parent.SetData("TeleportCoolDown", Time.time + cooldown);
        parent.SetData("isTeleportCooldown", true);
    }

    private void Teleport()
    {
        var direction = (_elfPos.position - _playerPos.position).normalized;
        var position = new Vector3(_elfPos.position.x + direction.x * _xDistanceTeleport,
            _elfPos.position.y + direction.y * _yDistanceTeleport, _elfPos.position.z);
        _elfPos.position = position;
    }

    public override NodeState Evaluate()
    {
        //Debug.Log("Teleport");
        if ((bool)GetData("isTeleportCooldown")) return NodeState.FAILURE;

        Teleport();

        startTeleportTimer(_teleportTimer);
        return NodeState.FAILURE;
    }
}