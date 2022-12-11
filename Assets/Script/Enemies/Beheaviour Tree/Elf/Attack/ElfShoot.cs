using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using Unity.VisualScripting;
using UnityEngine;

public class ElfShoot : Node
{

    private float _elfShootCoolDown;
    private int _maxShooting;

    private GameObject _bullet;
    private GameObject _elf;
    private GameObject _player;

    private float _bulletSpeed = 30f;
    private float _lifeTimeBullet = 3f;


    public ElfShoot(float ElfShootCoolDown, int MaxShooting, GameObject bullet, GameObject elf, GameObject player,
        float bulletSpeed, float lifeTimeBullet)
    {
        _elfShootCoolDown = ElfShootCoolDown;
        _maxShooting = MaxShooting;
        _bullet = bullet;
        _elf = elf;
        _player = player;
        _bulletSpeed = bulletSpeed;
        _lifeTimeBullet = lifeTimeBullet;
    }

    public override void initialize()
    {
        parent.SetData("isShootCooldown", false);
        parent.SetData("shootingCounter", 0);
        parent.SetData("maxShooting", _maxShooting);
    }
    
    private void startShootCoolDown(float cooldown)
    {
        parent.SetData("shootCoolDown", Time.time + cooldown);
        parent.SetData("isShootCooldown", true);
    }

    private void increaseShotCounter()
    {
        //Debug.Log(maxShooting);
        parent.SetData("shootingCounter", (int)GetData("shootingCounter") + 1 );
        //Debug.Log((int)GetData("shootingCounter"));
    }

    private void shoot()
    {
        Vector2 shootPosition = new Vector2(_elf.transform.position.x, _elf.transform.position.y);
        
        GameObject projectile = GameObject.Instantiate(_bullet, shootPosition, new Quaternion()) as GameObject;

        var direction = (_player.transform.position - _elf.transform.position).normalized;
        var velocity = _bulletSpeed * direction;
        projectile.GetComponent<Bullet>().SetVelocity(velocity);
        projectile.GetComponent<DieAfter>().lifeTime = _lifeTimeBullet;

    }

    public override NodeState Evaluate()
    {
        if ((bool)GetData("Awake") == false){
            SetDataRoot("Awake", true);
            _elf.GetComponent<Animator>().SetBool("Awake", true);
        }
    if ((bool) GetData("isShootCooldown")) return NodeState.FAILURE;
        if ((int)GetData("shootingCounter") >= _maxShooting)
        {
            parent.SetData("TeleportReady", true);
            return NodeState.FAILURE;
        }
        
        shoot();
        
        increaseShotCounter();
        startShootCoolDown(_elfShootCoolDown);
        return NodeState.SUCCESS;
    }
}
