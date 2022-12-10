using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAttack : PlayerAttack
{

    public GameObject throwPrefab;
    public float bulletSpeed = 30f;

    public override void DoAttack()
    {
        base.Attack();
        GameObject projectile = Instantiate(throwPrefab, throwPoint.position, Quaternion.identity);
        projectile.GetComponent<Bullet>().SetVelocity(bulletSpeed * aimDirection);
    }
}
