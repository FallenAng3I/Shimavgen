using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float zoomSpeed = 10f;
    public float minFOV = 20f;
    public float maxFOV = 60f;

    private Camera cam;
    private float originalFOV;

    void Start()
    {
        cam = GetComponent<Camera>();
        originalFOV = cam.fieldOfView;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            float newFOV = cam.fieldOfView - zoomSpeed * Time.deltaTime;
            newFOV = Mathf.Clamp(newFOV, minFOV, maxFOV);
            cam.fieldOfView = newFOV;
        }
        else
        {
            cam.fieldOfView = Mathf.MoveTowards(cam.fieldOfView, originalFOV, zoomSpeed * Time.deltaTime);
        }
    }
}