using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] private List<Transform> targets;
    [SerializeField] private int referenceScreenResolutionWidth = 1920;
    [SerializeField] private float maxResolutionScaleFactor = 1.2f, minResolutionScaleFactor = 0.8f;
    public Vector3 offset;
    public float smoothTime = 0.5f;
    private Vector3 velocity;
    public float maxZoom = 40f , minZoom = 10f, zoomLimiter = 50f;
    private Camera[] cam;
    private float _screenResolutionZoomFactor;

    private void Start()
    {
        cam = GetComponentsInChildren<Camera>();
    }


    private void FixedUpdate()
    {
        _screenResolutionZoomFactor = referenceScreenResolutionWidth;
        _screenResolutionZoomFactor /= Screen.width;
        _screenResolutionZoomFactor =
            Mathf.Clamp(_screenResolutionZoomFactor, minResolutionScaleFactor, maxResolutionScaleFactor);
        print(_screenResolutionZoomFactor);
        if (targets.Count == 0)
            return;
        Move();
        Zoom();
    }
    private void Move()
    {
        Vector3 centerPoint = GetCenterPoint();
        Vector3 newPosition = centerPoint + offset;
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }

    private void Zoom()
    {
        float newZoom = Mathf.Lerp(minZoom*_screenResolutionZoomFactor,maxZoom*_screenResolutionZoomFactor, GetGreatestDistance()/ zoomLimiter);
        // float newZoom = Mathf.Lerp(minZoom,maxZoom, GetGreatestDistance()/ zoomLimiter);
        foreach (var camera in cam)
        {
            camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, newZoom, Time.deltaTime);
        }
    }

    private Vector3 GetCenterPoint()
    {
        if (targets.Count == 1)
        {
            return targets[0].position;
        }
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for(int i =0; i< targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }
        return bounds.center;
    }

    private float GetGreatestDistance()
    {
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for(int i =0; i< targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }
        return bounds.size.x>bounds.size.y?bounds.size.x:bounds.size.y*2f;
    }
}
