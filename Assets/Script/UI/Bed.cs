using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bed : MonoBehaviour
{
    public GameData gameData;
    public TextMeshProUGUI text;
    public TextMeshProUGUI gameover;
    public TextMeshProUGUI score;

    void Update()
    {
        text.text = "Press E to Exchange " + gameData.teeth + " Teeth to " + gameData.teeth * 5 + " Score";
    }
    public void TeethToScore()
    {
        gameData.score = gameData.teeth * 5;
        gameData.teeth = 0;
        score.text = gameData.score.ToString();
        gameover.text = "You Done bro";
        Destroy(FindObjectOfType<Rosa.PlayerController>().gameObject);
        Destroy(gameObject);
    }
}
