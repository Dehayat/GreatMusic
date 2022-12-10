using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAttack : PlayerAttack
{

    public GameObject throwPrefab;
    public Transform throwPoint;
    public float bulletSpeed = 30f;

    private Vector2 aimDirection;

    public override void SetAimDirection(Vector2 dir)
    {
        //if (!listenForInput)
        //{
        //    aimInput = Vector2.right * facingDir;
        //    return;
        //}
        var aimInput = dir;
        Vector3 aimPosition = Camera.main.ScreenToWorldPoint(new Vector3(aimInput.x, aimInput.y, 0));
        aimPosition.z = 0;
        aimDirection = aimPosition - throwPoint.position;
        aimDirection.Normalize();
    }

    public override void Attack()
    {
        SetAimDirection(Input.mousePosition);
        GameObject projectile = Instantiate(throwPrefab, throwPoint.position, Quaternion.identity);
        //Rigidbody2D proRb = projectile.GetComponent<Rigidbody2D>();
        //proRb.AddForce(aimDirection * throwForce, ForceMode2D.Impulse);
        projectile.GetComponent<Bullet>().SetVelocity(bulletSpeed * aimDirection);
    }
}
