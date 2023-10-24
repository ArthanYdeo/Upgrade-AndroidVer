using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public float shakeDistance = 0.1f;
    public float shakeSpeed = 1;

    Vector2 initialPosition;
    Vector2 shakeOffset;

    bool isShaking = false;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isShaking)
        {
            Vector2 pos = transform.position;
            Vector2 offsetPos = pos + shakeOffset;
            float currentDistance = offsetPos.y - initialPosition.y;
            if (shakeSpeed >= 0)
            {
                if (currentDistance > shakeDistance)
                {
                    shakeSpeed *= -1;
                }
            }
            else
            {
                if (currentDistance < -shakeDistance)
                {
                    shakeSpeed *= -1;
                }
            }
            shakeOffset.y += shakeSpeed * Time.deltaTime;
            if (shakeOffset.y > shakeDistance) shakeOffset.y = shakeDistance;
            if (shakeOffset.y < -shakeDistance) shakeOffset.y = -shakeDistance;
            transform.position = initialPosition + shakeOffset;
        }
    }

    public void StartShaking()
    {
        isShaking = true;
    }

    public void StopShaking()
    {
        isShaking = false;
        transform.position = initialPosition;
    }
}
