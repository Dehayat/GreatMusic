using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteController : MonoBehaviour
{

    [SerializeField]
    private SpriteRenderer _renderer;
    [SerializeField]
    private Transform _transform;
    [SerializeField]
    private int frameFuxk = 15;


    private Vector3 _prevPos;
    private Vector3 _currPos;


    void Start()
    {
        _currPos = _transform.transform.position;
        _prevPos = _currPos;
    }

    void Update()
    {
        if (Time.frameCount % frameFuxk != 0) return;

        _currPos = _transform.transform.position;

        if ( _currPos.x < _prevPos.x)
        {
            _renderer.flipX = true;
        }
        else
        {
            _renderer.flipX = false;
        }

        _prevPos = _currPos;
    }
}
