using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyScale : MonoBehaviour
{
    public AnimationCurve spawnerCurve;
    public AnimationCurve spawnProbability;
    public Spawner[] spawners;
    public int updateFrequency = 10;
    public bool followGlobalTime = false;

    private void OnEnable()
    {
        EventSystem.GetInstance().ListenToEvent("UpdateTimeEvent", UpdateTime);
    }
    private void OnDisable()
    {
        EventSystem.GetInstance().IgnoreEvent("UpdateTimeEvent", UpdateTime);
    }

    private void UpdateTime(EventData obj)
    {
        if (followGlobalTime)
        {
            var timeData = obj as TimeEvent;
            SetTime(timeData.levelTime);
        }
    }

    private float t = 0;
    private void Update()
    {
        if (Time.frameCount % updateFrequency != 0)
        {
            return;
        }
        if (!followGlobalTime)
        {
            t += Time.deltaTime;
        }
        foreach (var spawner in spawners)
        {
            spawner.spawnInterval = spawnerCurve.Evaluate(t);
            spawner.spawnProbability = spawnProbability.Evaluate(t);
        }
    }

    public void SetTime(float time)
    {
        t = time;
    }
}
