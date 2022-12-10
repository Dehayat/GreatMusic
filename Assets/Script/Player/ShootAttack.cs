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

        float angle = Vector3.SignedAngle(Vector3.right, aimDirection, Vector3.forward);
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        GameObject projectile = Instantiate(throwPrefab, throwPoint.position, rotation);

        projectile.GetComponent<Bullet>().SetVelocity(bulletSpeed * aimDirection);
    }
}
