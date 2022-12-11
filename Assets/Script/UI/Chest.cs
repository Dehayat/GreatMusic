using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Chest : MonoBehaviour
{
    public GameData gameData;
    public AttackChooser attack;
    public TextMeshProUGUI text;

    void Update()
    {
        text.text = "Press E to switch weapons";
    }
    public void SwitchWeapon()
    {
        attack.SwitchWeapon();
    }
}
