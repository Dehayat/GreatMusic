using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DieAfter : MonoBehaviour
{


    public float lifeTime = 3f;
    public UnityEvent destroyEvent;
    private void Start()
    {
        StartCoroutine(Die(lifeTime));
    }

    IEnumerator Die(float t)
    {
        yield return new WaitForSeconds(t);
        destroyEvent?.Invoke();
        Destroy(gameObject);
    }
}
