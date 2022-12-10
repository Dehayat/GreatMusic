using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ToothType
{
    small,
    big,
    Huge
}

class TeethEvent : EventData
{
    public Vector3 dropPosition;
    public ToothType tooth;
    public TeethEvent(Vector3 pos, ToothType toothType)
    {
        dropPosition = pos;
        tooth = toothType;
    }
}

public class ToothDropper : MonoBehaviour
{
    public GameObject smallTeeth;
    public GameObject BigTeeth;
    public GameObject HugeTeeth;


    private void OnEnable()
    {
        EventSystem.GetInstance().ListenToEvent("DropTeeth", DropTeeth);
    }

    private void OnDisable()
    {
        EventSystem.GetInstance().IgnoreEvent("DropTeeth", DropTeeth);
    }

    private void DropTeeth(EventData obj)
    {
        TeethEvent teethData = obj as TeethEvent;
        GameObject teethPrefab = null;
        switch (teethData.tooth)
        {
            case ToothType.small:
                teethPrefab = smallTeeth;
                break;
            case ToothType.big:
                teethPrefab = BigTeeth;
                break;
            case ToothType.Huge:
                teethPrefab = HugeTeeth;
                break;
            default:
                break;
        }
        Instantiate(teethPrefab, teethData.dropPosition, Quaternion.identity);
    }
}
