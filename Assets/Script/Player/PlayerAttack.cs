using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    protected Vector2 aimDirection;
    public Transform throwOrigin;
    public Transform throwPoint;
    public float throwCooldown = 0.5f;
    public float aimTime = 0.2f;



    private float lastAttack = 0;
    Quaternion savedRotation;

    protected bool useDmg = false;
    protected float damageMultiplier;
    public void SetAttackDamage(float dmg)
    {
        useDmg = true;
        damageMultiplier = dmg;
    }
    public void ResetAttackDamage()
    {
        useDmg = false;
    }

    private void OnEnable()
    {
        EventSystem.GetInstance().ListenToEvent("UpgradeAttackDamage", UpgradeGunDamage);
    }
    private void OnDisable()
    {
        EventSystem.GetInstance().IgnoreEvent("UpgradeAttackDamage", UpgradeGunDamage);
    }
    private Coroutine DamageUpgradeCo;
    private void UpgradeGunDamage(EventData obj)
    {
        FloatEventData eventData = obj as FloatEventData;
        if (DamageUpgradeCo != null)
        {
            ResetAttackDamage();
            StopCoroutine(DamageUpgradeCo);
        }
        DamageUpgradeCo = StartCoroutine(SetDamageForDuration(eventData.value1, eventData.value2));
    }
    IEnumerator SetDamageForDuration(float damageMulti, float duration)
    {
        SetAttackDamage(damageMulti);
        yield return new WaitForSeconds(duration);
        ResetAttackDamage();
    }


    public void Attack()
    {
        if (lastAttack + throwCooldown > Time.time)
            return;
        lastAttack = Time.time;
        SetAimDirection(Input.mousePosition);
        float angle = Vector3.SignedAngle(Vector3.right, aimDirection, Vector3.forward);
        savedRotation = throwOrigin.transform.rotation;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        throwOrigin.transform.rotation = rotation;
        StartCoroutine(StopAiming());
        DoAttack();
    }
    IEnumerator StopAiming()
    {
        yield return new WaitForSeconds(aimTime);
        throwOrigin.transform.rotation = savedRotation;
    }
    public virtual void SetAimDirection(Vector2 dir)
    {
        var aimInput = dir;
        Vector3 aimPosition = Camera.main.ScreenToWorldPoint(new Vector3(aimInput.x, aimInput.y, 0));
        aimPosition.z = 0;
        aimDirection = aimPosition - throwOrigin.position;
        aimDirection.Normalize();
    }
    public virtual void DoAttack() { }
}
