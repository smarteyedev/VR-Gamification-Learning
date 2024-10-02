using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunRotation : MonoBehaviour
{
    public float dayLength = 30f;

    public Vector3 initialRotation;
    void Start()
    {
        transform.rotation = Quaternion.Euler(initialRotation);
    }

    void Update()
    {
        float dayProgress = (Time.time % dayLength) / dayLength;

        float sunAngle = dayProgress * 360f;
        transform.rotation = Quaternion.Euler(sunAngle - 90f, 0f, 0f);
    }
}
