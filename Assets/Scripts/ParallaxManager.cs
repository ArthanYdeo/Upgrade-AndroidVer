using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxManager : MonoBehaviour
{
     public float parallaxSpeed = 0.5f; // Adjust this value to control the speed of the layer

    private Transform cameraTransform;
    private Vector3 previousCameraPosition;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        previousCameraPosition = cameraTransform.position;
    }

    private void Update()
    {
        float parallax = (previousCameraPosition.x - cameraTransform.position.x) * parallaxSpeed;
        float backgroundTargetPosX = transform.position.x + parallax;
        Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, transform.position.y, transform.position.z);

        transform.position = Vector3.Lerp(transform.position, backgroundTargetPos, Time.deltaTime);
        previousCameraPosition = cameraTransform.position;
    }
}

