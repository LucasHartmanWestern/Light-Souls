using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    // Speed at which the camera rotates
    public float rotationSpeed = 10f;

    // Update is called once per frame
    void Update()
    {
        // Rotate the camera around the Y-axis
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
