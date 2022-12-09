using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;
    public float dampTimeX = 0.2f;
    public float dampTimeY = 0.2f;
    public float maxSpeedX = 15f;
    public float maxSpeedY = 10f;
    public Vector2 targetOffset = Vector2.zero;
    public float returnFromOffsetTimeX = 0.1f;


    public float returnFromOffsetTimeY = 0.1f;
    public Camera cam;



    private void Awake()
    {
        savedZ = transform.position.z;
    }

    private float savedZ;
    private void Start()
    {
        currentOffset = targetOffset;
        transform.position = ClampPositionInLevel(target.position);
    }


    private Vector3 dampVelocityX = Vector3.zero;
    private Vector3 dampVelocityY = Vector3.zero;
    private Vector2 currentOffset;
    private float dampOffsetX = 0f;
    private float dampOffsetY = 0f;
    private void FixedUpdate()
    {
        Vector3 targetPosition = target.position;
        targetPosition = ClampPositionInLevel(targetPosition);
        targetPosition = ClampPositionInZone(targetPosition);
        {
            currentOffset.x = Mathf.SmoothDamp(currentOffset.x, targetOffset.x, ref dampOffsetX, returnFromOffsetTimeX);
            currentOffset.y = Mathf.SmoothDamp(currentOffset.y, targetOffset.y, ref dampOffsetY, returnFromOffsetTimeY);
        }
        targetPosition.x += currentOffset.x;
        targetPosition.y += currentOffset.y;
        targetPosition.z = savedZ;
        Vector3 targetX = transform.position;
        targetX.x = targetPosition.x;
        Vector3 targetY = transform.position;
        targetY.y = targetPosition.y;
        Vector3 destinationX = Vector3.SmoothDamp(transform.position, targetX, ref dampVelocityX, dampTimeX, maxSpeedX);
        Vector3 destinationY = Vector3.SmoothDamp(transform.position, targetY, ref dampVelocityY, dampTimeY, maxSpeedY);
        transform.position = new Vector3(destinationX.x, destinationY.y, savedZ);

        FixZ();
    }

    private Vector3 ClampPositionInLevel(Vector3 targetPosition)
    {
        return targetPosition;
    }

    private Vector3 ClampPositionInZone(Vector3 targetPosition)
    {
        return targetPosition;
    }
    private void FixZ()
    {
        Vector3 position = transform.position;
        position.z = savedZ;
        transform.position = position;
    }

    public void SnapToTarget()
    {
        transform.position = ClampPositionInZone(ClampPositionInLevel(target.position));
    }
}
