using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FuckShitUp());
    }
    IEnumerator FuckShitUp()
    {
        yield return new WaitForSecondsRealtime(10);
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
