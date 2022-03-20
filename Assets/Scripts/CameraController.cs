using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target2;
    public PlayerController player;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    Vector3 turnSmoothVelocity;
    public float currentZoom = 10f;
    public float zoomSpeed = 4f;
    public float minZoom = 8f;
    public float maxZoom = 20f;

    public float range;
    public Vector3 distance;
    private void Update()
    {
        distance = player.gameObject.transform.position - target2.position;
        range = Mathf.Sqrt(distance.x * distance.x + distance.z * distance.z) * 0.5f;

        currentZoom = Mathf.Clamp(range, minZoom, maxZoom);
    }

    void LateUpdate()
    {
        Vector3 desiredPos = (player.gameObject.transform.position + target2.position)/2 + offset * currentZoom;
        Vector3 smoothPos = Vector3.SmoothDamp(transform.position, desiredPos, ref turnSmoothVelocity , smoothSpeed);
        transform.position = smoothPos;
    }

}
