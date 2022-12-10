using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAttack : PlayerAttack
{

    public GameObject throwPrefab;
    public Transform throwPoint;
    public float bulletSpeed = 30f;
    public float throwCooldown = 0.5f;

    private Vector2 aimDirection;

    public override void SetAimDirection(Vector2 dir)
    {
        var aimInput = dir;
        Vector3 aimPosition = Camera.main.ScreenToWorldPoint(new Vector3(aimInput.x, aimInput.y, 0));
        aimPosition.z = 0;
        aimDirection = aimPosition - throwPoint.position;
        aimDirection.Normalize();
    }

    private float lastAttack = 0;
    public override void Attack()
    {
        if (lastAttack + throwCooldown > Time.time)
            return;
        lastAttack = Time.time;
        SetAimDirection(Input.mousePosition);
        GameObject projectile = Instantiate(throwPrefab, throwPoint.position, Quaternion.identity);
        projectile.GetComponent<Bullet>().SetVelocity(bulletSpeed * aimDirection);
    }
}
