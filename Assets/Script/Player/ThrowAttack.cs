using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowAttack : PlayerAttack
{

    public GameObject throwPrefab;
    public float throwForce = 10f;

    public override void DoAttack()
    {
        GameObject projectile = Instantiate(throwPrefab, throwPoint.position, Quaternion.identity);
        Rigidbody2D proRb = projectile.GetComponent<Rigidbody2D>();
        proRb.AddForce(aimDirection * throwForce, ForceMode2D.Impulse);
    }
}
