using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeEvent : EventData
{
    public float levelTime;
    public float freshLevelTime;
}
public class ScoreAddEvent : EventData
{
    public float baseScore;

    public ScoreAddEvent(float baseScore)
    {
        this.baseScore = baseScore;
    }
}

public class GameData : MonoBehaviour
{
    public float levelTime = 0f;
    public float freshLevelTime = 0f;
    public float currentScoreMultiplier = 1f;
    public int score = 0;
    public int currentWeapon = 0;
    public bool isInLevel = false;
    public int teeth = 0;

    public AnimationCurve scoreMultiplierCurve;

    private void OnEnable()
    {
        EventSystem.GetInstance().ListenToEvent("ScoreEvent", AddScore);
    }

    private void AddScore(EventData obj)
    {
        ScoreAddEvent eventData = obj as ScoreAddEvent;
        score += (int)(eventData.baseScore * (int)currentScoreMultiplier);
    }

    private void OnDisable()
    {
        EventSystem.GetInstance().IgnoreEvent("ScoreEvent", AddScore);
    }


    private void Start()
    {
        EnterLevel();
    }

    private void Update()
    {
        if (isInLevel)
        {
            levelTime += Time.deltaTime;
            freshLevelTime += Time.deltaTime;
            currentScoreMultiplier = scoreMultiplierCurve.Evaluate(freshLevelTime);

            EventSystem.GetInstance().EmitEvent("UpdateTimeEvent", new TimeEvent
            {
                levelTime = this.levelTime,
                freshLevelTime = this.freshLevelTime
            });
        }
    }
    public void EnterLevel()
    {
        isInLevel = true;
        freshLevelTime = 0f;
        currentScoreMultiplier = 1f;
    }
    public void ExitLevel()
    {
        isInLevel = false;
    }
}
