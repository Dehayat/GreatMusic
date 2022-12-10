using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatEventData : EventData
{
    public float value1;
    public float value2;
}


public class Upgrade : MonoBehaviour
{
    public bool doThing = false;
    public float duration = 5;

    private void Update()
    {
        if (doThing)
        {
            doThing = false;
            SetAttackDamage(5);
        }
    }

    public void SetAttackCoolDown(float coolDown)
    {
        EventSystem.GetInstance().EmitEvent("UpgradeAttackCoolDown", new FloatEventData { value1 = coolDown, value2 = duration });
    }
    public void SetAttackDamage(float damage)
    {
        EventSystem.GetInstance().EmitEvent("UpgradeAttackDamage", new FloatEventData { value1 = damage, value2 = duration });

    }
    public void SetMoveSpeed(float speed)
    {
        EventSystem.GetInstance().EmitEvent("UpgradeMoveSpeed", new FloatEventData { value1 = speed, value2 = duration });
    }
}
