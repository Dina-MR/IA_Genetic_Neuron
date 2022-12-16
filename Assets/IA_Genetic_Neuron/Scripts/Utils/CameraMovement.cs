using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _endPositionZ; // ZAxis position where the camera stops moving (when it reaches the goal)
    private Vector3 _initialPosition;

    private void Start()
    {
        _initialPosition = transform.position;
    }

    void Update()
    {
        MoveCamera();
        if(transform.position.z >= _endPositionZ)
            enabled = false;
    }

    // Movment of the camera
    private void MoveCamera()
    {
        float step = _speed * Time.deltaTime;
        transform.position += Vector3.forward * step;
    }

    // Reset the position when a new race is about to start
    public void ResetPosition()
    {
        transform.position = _initialPosition;
        enabled = true;
    }
}
