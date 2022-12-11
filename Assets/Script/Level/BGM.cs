using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class BGM : MonoBehaviour
{

    public GameData data;

    public float secondPhaseStart = 40;
    public float thirdPhaseStart = 120;
    public StudioEventEmitter bgm;

    private void Update()
    {
        if (data.levelTime < secondPhaseStart)
        {
            bgm.SetParameter("Zone", 1);
        }
        else if (data.levelTime < thirdPhaseStart)
        {
            bgm.SetParameter("Zone", 2);
        }
        else
        {
            bgm.SetParameter("Zone", 3);
        }
    }
}
