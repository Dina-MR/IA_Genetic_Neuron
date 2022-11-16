using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float _speed;

    void Update()
    {
        MoveCamera();
    }

    // Mouvement de la caméra
    private void MoveCamera()
    {
        float step = _speed * Time.deltaTime;
        transform.position += Vector3.forward * step;
    }
}
