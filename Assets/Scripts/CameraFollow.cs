using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector2 offset = Vector2.zero;
   // public Vector2 minBounds = new Vector2(-10f, -10f);
    //public Vector2 maxBounds = new Vector2(10f, 10f);

    void LateUpdate()
    {
        if (target != null)
        {
            Vector2 desiredPosition = (Vector2)target.position + offset;
            Vector2 smoothedPosition = Vector2.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Clamp the camera position within the specified bounds
           // smoothedPosition.x = Mathf.Clamp(smoothedPosition.x, minBounds.x, maxBounds.x);
           // smoothedPosition.y = Mathf.Clamp(smoothedPosition.y, minBounds.y, maxBounds.y);

            transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z);

            /*// Debug logs for position information
            Debug.Log("Player Position: " + target.position);
            Debug.Log("Camera Position: " + transform.position);
            Debug.Log("Desired Position: " + desiredPosition);*/
        }
    }
}
