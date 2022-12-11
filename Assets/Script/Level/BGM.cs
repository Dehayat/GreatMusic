using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class BGM : MonoBehaviour
{

    public GameData data;

    public float secondPhaseStart = 40;
    public StudioEventEmitter bgm;

    private void Update()
    {
        if (data.isInLevel)
        {
            bgm.EventInstance.setParameterByName("Zone", 1);
        }
        else
        {
            bgm.EventInstance.setParameterByName("Zone", 0);
        }

        if (data.levelTime < secondPhaseStart)
        {
            bgm.EventInstance.setParameterByName("Phase", 1);
        }
        else
        {
            bgm.EventInstance.setParameterByName("Phase", 3);
        }
    }
}
