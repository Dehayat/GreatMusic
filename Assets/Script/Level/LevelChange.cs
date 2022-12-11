using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChange : MonoBehaviour
{
    GameData gamedata;

    private void Start()
    {
        gamedata = FindObjectOfType<GameData>();
    }

    public void GoToLevel()
    {
        ClearLevel();
        gamedata.EnterLevel();
    }

    public void GoToHub()
    {
        ClearLevel();
        gamedata.ExitLevel();
    }

    private void ClearLevel()
    {
        var pickups = FindObjectsOfType<Upgrade>();
        foreach (var tooth in pickups)
        {
            Destroy(tooth.gameObject);
        }
        var teeth = FindObjectsOfType<Tooth>();
        foreach (var tooth in teeth)
        {
            Destroy(tooth.gameObject);
        }
        var enemies = FindObjectsOfType<EnemyController>();
        foreach (var enemy in enemies)
        {
            Destroy(enemy.gameObject);
        }
    }
}
