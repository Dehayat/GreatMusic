using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackUpgrades : MonoBehaviour
{
    private AttackChooser attackChooser;

    private void Awake()
    {
        attackChooser = GetComponent<AttackChooser>();
    }

    private void OnEnable()
    {
        //EventSystem.GetInstance().ListenToEvent("UpgradeAttackDamage", UpgradeGunCooldown);
        EventSystem.GetInstance().ListenToEvent("UpgradeAttackCoolDown", UpgradeGunCooldown);
    }
    private void OnDisable()
    {
        //EventSystem.GetInstance().IgnoreEvent("UpgradeAttackDamage", UpgradeGunCooldown);
        EventSystem.GetInstance().IgnoreEvent("UpgradeAttackCoolDown", UpgradeGunCooldown);
    }

    private Coroutine cooldownUpgradeCo;
    private void UpgradeGunCooldown(EventData obj)
    {
        FloatEventData eventData = obj as FloatEventData;
        if (cooldownUpgradeCo != null)
        {
            attackChooser.GetCurrentAttack().throwCooldown = savedCooldown;
            StopCoroutine(cooldownUpgradeCo);
        }
        cooldownUpgradeCo = StartCoroutine(SetCooldownForDuration(eventData.value1, eventData.value2));
    }
    private float savedCooldown;
    IEnumerator SetCooldownForDuration(float coolDown, float duration)
    {
        savedCooldown = attackChooser.GetCurrentAttack().throwCooldown;
        attackChooser.GetCurrentAttack().throwCooldown = coolDown;
        yield return new WaitForSeconds(duration);
        attackChooser.GetCurrentAttack().throwCooldown = savedCooldown;
    }
}
