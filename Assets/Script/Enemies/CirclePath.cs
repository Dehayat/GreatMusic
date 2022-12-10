using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirclePath : MonoBehaviour
{
    public Transform _transform;

    public Vector3 target;

    public float rotationSpeed = 1;

    public float CircleRadius = 1;

    public float ElevationOffset = 0;
    
    private Vector3 positionOffset;
    private float angle = 10f;
    
    // Start is called before the first frame update
    void Start()
    {
        _transform = transform;
        if (target == Vector3.zero) target = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        positionOffset.Set(
            Mathf.Cos( angle ) * CircleRadius,
            Mathf.Sin( angle ) * CircleRadius,
            0
            
        );
        _transform.position = target + positionOffset;
        angle += Time.deltaTime * rotationSpeed;
        
    }
}
