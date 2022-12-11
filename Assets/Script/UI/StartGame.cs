using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    bool play = false;
    // Update is called once per frame
    void Update()
    {
        if (play) return;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            play = true;
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }
    }
}
