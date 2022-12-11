using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bed : MonoBehaviour
{
    public GameData gameData;
    public TextMeshProUGUI text;

    void Update()
    {
        text.text = "Press E to Exchange " + gameData.teeth + " Teeth to " + gameData.teeth * 5 + " Score";
    }
    public void TeethToScore()
    {
        gameData.score = gameData.teeth * 5;
        gameData.teeth = 0;
    }
}
