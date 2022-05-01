using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFacer : MonoBehaviour
{
    public Camera CameraToFace;

    private void Start()
    {
        if (!CameraToFace)
            CameraToFace = Camera.main;
    }

    private void LateUpdate()
    {
        var rotation = CameraToFace.transform.rotation;
        transform.LookAt(transform.position + rotation * Vector3.forward, Vector3.up);
    }
}
