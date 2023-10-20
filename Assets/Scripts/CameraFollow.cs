using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float smoothTime = 0.25f;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset = new Vector3 (-2, 0, 0);

    private void Start()
    {
    }

    private void FixedUpdate()
    {
        // Get the target position
        Vector3 targetPosition = new Vector3(target.position.x + offset.x, transform.position.y, transform.position.z);

        // Smoothly move the camera towards the target position
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
